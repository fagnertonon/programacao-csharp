// ====================================================================
//  O DESENHISTA - WebGL puro, sem biblioteca nenhuma.
//
//  Voce NAO precisa mexer aqui, e nem precisa entender. A turma nunca
//  viu JavaScript, e nao vai ver hoje: o que voce escreve e C#.
//
//  Por que WebGL cru e nao uma biblioteca 3D pronta? Porque biblioteca
//  vem de CDN, CDN precisa de internet, e a internet do laboratorio cai.
//  Este arquivo tem 300 linhas e nao depende de nada.
// ====================================================================

const Render = (function () {
  let gl, prog, canvas;
  let bufPos, bufCor, bufNorm, verts = 0;
  let bufLinha, linhas = 0;
  let mundo = null;              // {largura, altura, fundo, tipos, celulas}
  let visual = {                 // as cores do MinhaConfig.cs do aluno
    ceu: [0.42, 0.63, 0.85], roupa: [0.30, 0.42, 0.85],
    pele: [0.92, 0.76, 0.60], cabelo: [0.36, 0.24, 0.16],
    calca: [0.17, 0.23, 0.42]
  };
  let cores = [];                // por indice de tipo, em [r,g,b]

  // ------------------------------------------------------------------
  //  MATEMATICA DE MATRIZ - o minimo para ter camera
  // ------------------------------------------------------------------

  function multiplicar(a, b) {
    const o = new Float32Array(16);
    for (let i = 0; i < 4; i++)
      for (let j = 0; j < 4; j++) {
        let s = 0;
        for (let k = 0; k < 4; k++) s += a[i * 4 + k] * b[k * 4 + j];
        o[i * 4 + j] = s;
      }
    return o;
  }

  function perspectiva(fov, aspecto, perto, longe) {
    const f = 1 / Math.tan(fov / 2);
    return new Float32Array([
      f / aspecto, 0, 0, 0,
      0, f, 0, 0,
      0, 0, (longe + perto) / (perto - longe), -1,
      0, 0, (2 * longe * perto) / (perto - longe), 0
    ]);
  }

  function olhar(olho, alvo, cima) {
    const z = normalizar(subtrair(olho, alvo));
    const x = normalizar(cruzar(cima, z));
    const y = cruzar(z, x);
    return new Float32Array([
      x[0], y[0], z[0], 0,
      x[1], y[1], z[1], 0,
      x[2], y[2], z[2], 0,
      -(x[0] * olho[0] + x[1] * olho[1] + x[2] * olho[2]),
      -(y[0] * olho[0] + y[1] * olho[1] + y[2] * olho[2]),
      -(z[0] * olho[0] + z[1] * olho[1] + z[2] * olho[2]), 1
    ]);
  }

  const subtrair = (a, b) => [a[0] - b[0], a[1] - b[1], a[2] - b[2]];
  const cruzar = (a, b) => [
    a[1] * b[2] - a[2] * b[1],
    a[2] * b[0] - a[0] * b[2],
    a[0] * b[1] - a[1] * b[0]
  ];
  function normalizar(v) {
    const n = Math.hypot(v[0], v[1], v[2]) || 1;
    return [v[0] / n, v[1] / n, v[2] / n];
  }

  // ------------------------------------------------------------------
  //  A CAMERA: orbita em volta do jogador
  // ------------------------------------------------------------------

  function posicaoDaCamera(cam) {
    const cp = Math.cos(cam.pitch), sp = Math.sin(cam.pitch);
    return [
      cam.alvo[0] + cam.dist * cp * Math.sin(cam.yaw),
      cam.alvo[1] + cam.dist * sp,
      cam.alvo[2] + cam.dist * cp * Math.cos(cam.yaw)
    ];
  }

  // A CAMERA NAO PODE FICAR ATRAS DE UM BLOCO.
  //
  // Isto foi um defeito de verdade, e chato: bastava o boneco andar para
  // debaixo de uma arvore e ele sumia da tela - a copa ficava entre a
  // camera e ele, e o aluno perdia o proprio personagem.
  //
  // A solucao e a de qualquer jogo em terceira pessoa: caminhar do
  // jogador ATE a camera e, no primeiro cubo solido do caminho, puxar a
  // camera para ca. Assim ela chega perto quando o espaco e apertado e
  // volta a se afastar quando o caminho abre.
  // Agua e folha nao empurram a camera: sao os dois blocos que o jogo ja
  // trata como atravessaveis, e a copa de arvore era justamente a maior
  // culpada por esconder o boneco.
  let vazados = {};
  let folhaIdx = -1;
  let jogadorCelula = null;   // onde o boneco esta, para abrir o teto

  function calcularVazados(m) {
    vazados = { 0: true };
    folhaIdx = -1;
    m.tipos.forEach((t, i) => {
      if (t.nome === 'agua' || t.nome === 'folha' || t.nome === 'ar') {
        vazados[i] = true;
      }
      if (t.nome === 'folha') { folhaIdx = i; }
    });
  }

  function distanciaLivre(cam) {
    if (!mundo) { return cam.dist; }

    const cp = Math.cos(cam.pitch), sp = Math.sin(cam.pitch);
    const dir = [cp * Math.sin(cam.yaw), sp, cp * Math.cos(cam.yaw)];

    const passo = 0.2;
    for (let d = passo; d < cam.dist; d += passo) {
      const x = Math.floor(cam.alvo[0] + dir[0] * d);
      const y = Math.floor(cam.alvo[1] + dir[1] * d);
      const z = Math.floor(cam.alvo[2] + dir[2] * d);

      // Fora da caixa do mundo nao atrapalha: e ceu.
      if (x < 0 || x >= mundo.largura || z < 0 || z >= mundo.fundo) { continue; }
      if (y < 0 || y >= mundo.altura) { continue; }

      if (!vazados[tipoEm(x, y, z)]) {
        // Para ANTES do bloco. O 1.6 e so um chao para a camera nao
        // atravessar o proprio boneco - e nunca pode ser maior que a
        // distancia do obstaculo, senao ela entra dentro dele. Foi
        // exatamente esse o defeito da primeira versao: com um piso de
        // 4.5 e uma parede a 1 de distancia, a camera ia parar do outro
        // lado e a tela virava um paredao de uma cor so.
        return Math.max(1.6, Math.min(d - 0.35, cam.dist));
      }
    }
    return cam.dist;
  }

  function matrizes(cam) {
    const livre = { alvo: cam.alvo, yaw: cam.yaw, pitch: cam.pitch,
                    dist: distanciaLivre(cam) };
    const olho = posicaoDaCamera(livre);
    const p = perspectiva(1.0, canvas.width / canvas.height, 0.1, 200);
    const v = olhar(olho, cam.alvo, [0, 1, 0]);
    return { olho: olho, mvp: multiplicar(v, p) };
  }

  // ------------------------------------------------------------------
  //  PARTIDA
  // ------------------------------------------------------------------

  const VS = `
    attribute vec3 aPos; attribute vec3 aCor; attribute vec3 aNorm;
    uniform mat4 uMVP; varying vec3 vCor;
    void main() {
      // A luz nao vem de calculo: cada face ganha um brilho fixo pela
      // direcao dela. E o truque que da a cara de voxel.
      float luz = 0.55
                + 0.45 * max(aNorm.y, 0.0)
                + 0.16 * abs(aNorm.x)
                + 0.08 * abs(aNorm.z);
      vCor = aCor * min(luz, 1.25);
      gl_Position = uMVP * vec4(aPos, 1.0);
    }`;

  const FS = `
    precision mediump float; varying vec3 vCor;
    void main() { gl_FragColor = vec4(vCor, 1.0); }`;

  function compilar(tipo, fonte) {
    const s = gl.createShader(tipo);
    gl.shaderSource(s, fonte);
    gl.compileShader(s);
    if (!gl.getShaderParameter(s, gl.COMPILE_STATUS)) {
      throw new Error('shader: ' + gl.getShaderInfoLog(s));
    }
    return s;
  }

  function iniciar(elemento) {
    canvas = elemento;
    // preserveDrawingBuffer: sem ele o navegador joga fora o quadro logo
    // depois de desenhar, e qualquer captura de tela do jogo sai preta ou
    // pela metade. Custa um pouco de desempenho e vale a pena.
    gl = canvas.getContext('webgl', { antialias: true, preserveDrawingBuffer: true });
    if (!gl) { throw new Error('Este navegador nao tem WebGL.'); }

    prog = gl.createProgram();
    gl.attachShader(prog, compilar(gl.VERTEX_SHADER, VS));
    gl.attachShader(prog, compilar(gl.FRAGMENT_SHADER, FS));
    gl.linkProgram(prog);
    gl.useProgram(prog);

    bufPos = gl.createBuffer();
    bufCor = gl.createBuffer();
    bufNorm = gl.createBuffer();
    bufLinha = gl.createBuffer();

    gl.enable(gl.DEPTH_TEST);
    gl.enable(gl.CULL_FACE);
    gl.cullFace(gl.BACK);
  }

  // ------------------------------------------------------------------
  //  O MUNDO
  // ------------------------------------------------------------------

  function hexParaRgb(hex) {
    const n = parseInt((hex || '#888888').slice(1), 16);
    return [((n >> 16) & 255) / 255, ((n >> 8) & 255) / 255, (n & 255) / 255];
  }

  function rgbParaHex(v) {
    const n = (c) => Math.round(Math.max(0, Math.min(1, c)) * 255)
                       .toString(16).padStart(2, '0');
    return '#' + n(v[0]) + n(v[1]) + n(v[2]);
  }

  function indice(x, y, z) {
    return y * mundo.fundo * mundo.largura + z * mundo.largura + x;
  }

  function tipoEm(x, y, z) {
    if (x < 0 || x >= mundo.largura) return 0;
    if (y < 0 || y >= mundo.altura) return 0;
    if (z < 0 || z >= mundo.fundo) return 0;
    return mundo.celulas[indice(x, y, z)];
  }

  function carregarMundo(m) {
    mundo = m;
    cores = m.tipos.map(t => hexParaRgb(t.cor));
    calcularVazados(m);
    Bichos.guardarMeuMonstro(m.monstro);

    const c = m.config;
    if (c) {
      visual = {
        ceu: hexParaRgb(c.ceu), roupa: hexParaRgb(c.roupa),
        pele: hexParaRgb(c.pele), cabelo: hexParaRgb(c.cabelo),
        calca: hexParaRgb(c.calca)
      };
    }
    remontar();
  }

  function mudar(x, y, z, tipoNome) {
    if (!mundo) return;
    let i = mundo.tipos.findIndex(t => t.nome === tipoNome);
    if (tipoNome === '' || tipoNome === null || tipoNome === undefined) i = 0;
    if (i < 0) i = 0;
    mundo.celulas[indice(x, y, z)] = i;
  }

  // As seis faces do cubo: quatro cantos e a normal de cada uma.
  const FACES = [
    { n: [0, 1, 0], v: [[0, 1, 0], [0, 1, 1], [1, 1, 1], [1, 1, 0]], d: [0, 1, 0] },
    { n: [0, -1, 0], v: [[0, 0, 0], [1, 0, 0], [1, 0, 1], [0, 0, 1]], d: [0, -1, 0] },
    { n: [0, 0, 1], v: [[0, 0, 1], [1, 0, 1], [1, 1, 1], [0, 1, 1]], d: [0, 0, 1] },
    { n: [0, 0, -1], v: [[1, 0, 0], [0, 0, 0], [0, 1, 0], [1, 1, 0]], d: [0, 0, -1] },
    { n: [1, 0, 0], v: [[1, 0, 1], [1, 0, 0], [1, 1, 0], [1, 1, 1]], d: [1, 0, 0] },
    { n: [-1, 0, 0], v: [[0, 0, 0], [0, 0, 1], [0, 1, 1], [0, 1, 0]], d: [-1, 0, 0] }
  ];

  // Monta a malha inteira do mundo, pulando toda face que tem cubo
  // colado nela - so o que da para ver vira triangulo. Sem isso seriam
  // 11520 cubos x 6 faces; com isso sobram uns 3 mil.
  // A COPA ABRE QUANDO VOCE ENTRA DEBAIXO DELA.
  //
  // Isto foi um defeito de verdade e dos piores: bastava o boneco andar
  // para debaixo de uma arvore e ele sumia da tela - a copa ficava entre
  // a camera e ele. Empurrar a camera nao resolve (a folha esta em cima,
  // nao atras), entao a solucao e nao DESENHAR as folhas que estao logo
  // acima do jogador. E o mesmo truque do telhado que abre nos jogos
  // isometricos.
  function folhaEscondida(x, y, z) {
    if (!jogadorCelula) { return false; }
    if (y <= jogadorCelula.y + 1) { return false; }
    if (y > jogadorCelula.y + 7) { return false; }
    return Math.abs(x - jogadorCelula.x) <= 3 && Math.abs(z - jogadorCelula.z) <= 3;
  }

  // Chamado quando o boneco muda de casa. Só remonta a malha se a copa
  // que precisa sumir mudou - senão seriam 11 mil cubos a cada passo.
  function seguirJogador(j) {
    if (!j) { return; }
    const igual = jogadorCelula && jogadorCelula.x === j.x
               && jogadorCelula.y === j.y && jogadorCelula.z === j.z;
    if (igual) { return; }

    const tinhaTeto = temFolhaPerto(jogadorCelula);
    jogadorCelula = { x: j.x, y: j.y, z: j.z };
    const temTeto = temFolhaPerto(jogadorCelula);

    if (tinhaTeto || temTeto) { remontar(); }
  }

  function temFolhaPerto(c) {
    if (!c || !mundo || folhaIdx < 0) { return false; }
    for (let y = c.y + 2; y <= c.y + 7; y++)
      for (let x = c.x - 3; x <= c.x + 3; x++)
        for (let z = c.z - 3; z <= c.z + 3; z++)
          if (tipoEm(x, y, z) === folhaIdx) { return true; }
    return false;
  }

  function remontar() {
    const P = [], C = [], N = [];

    for (let y = 0; y < mundo.altura; y++)
      for (let z = 0; z < mundo.fundo; z++)
        for (let x = 0; x < mundo.largura; x++) {
          const t = tipoEm(x, y, z);
          if (t === 0) continue;
          if (t === folhaIdx && folhaEscondida(x, y, z)) continue;
          const cor = cores[t] || [0.6, 0.6, 0.6];

          for (const f of FACES) {
            if (tipoEm(x + f.d[0], y + f.d[1], z + f.d[2]) !== 0) continue;

            const q = [f.v[0], f.v[1], f.v[2], f.v[0], f.v[2], f.v[3]];
            for (const v of q) {
              P.push(x + v[0], y + v[1], z + v[2]);
              C.push(cor[0], cor[1], cor[2]);
              N.push(f.n[0], f.n[1], f.n[2]);
            }
          }
        }

    // O jogador, desenhado como dois cubos - corpo e cabeca.
    verts = P.length / 3;
    enviar(bufPos, P);
    enviar(bufCor, C);
    enviar(bufNorm, N);
  }

  function enviar(buf, dados) {
    gl.bindBuffer(gl.ARRAY_BUFFER, buf);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(dados), gl.STATIC_DRAW);
  }

  function ligar(buf, nome) {
    gl.bindBuffer(gl.ARRAY_BUFFER, buf);
    const a = gl.getAttribLocation(prog, nome);
    gl.enableVertexAttribArray(a);
    gl.vertexAttribPointer(a, 3, gl.FLOAT, false, 0, 0);
  }

  // ------------------------------------------------------------------
  //  A MIRA: percorre a linha do olho ate bater num cubo (algoritmo DDA)
  // ------------------------------------------------------------------

  function mirar(cam, mx, my) {
    if (!mundo) return null;

    const { olho, mvp } = matrizes(cam);

    // Do pixel do mouse para uma direcao no mundo: desfaz a projecao
    // em dois pontos da linha e subtrai um do outro.
    const inv = inverter(mvp);
    if (!inv) return null;

    const ndcX = (mx / canvas.clientWidth) * 2 - 1;
    const ndcY = 1 - (my / canvas.clientHeight) * 2;

    const perto = aplicar(inv, [ndcX, ndcY, -1, 1]);
    const longe = aplicar(inv, [ndcX, ndcY, 1, 1]);
    const dir = normalizar(subtrair(longe, perto));

    let px = olho[0], py = olho[1], pz = olho[2];
    let anterior = null;
    const passo = 0.06;

    for (let i = 0; i < 1400; i++) {
      const cx = Math.floor(px), cy = Math.floor(py), cz = Math.floor(pz);

      if (tipoEm(cx, cy, cz) !== 0) {
        return { alvo: { x: cx, y: cy, z: cz }, vizinho: anterior };
      }
      if (cx >= 0 && cx < mundo.largura && cy >= 0 && cy < mundo.altura
          && cz >= 0 && cz < mundo.fundo) {
        anterior = { x: cx, y: cy, z: cz };
      }

      px += dir[0] * passo; py += dir[1] * passo; pz += dir[2] * passo;
    }
    return null;
  }

  function aplicar(m, v) {
    const o = [0, 0, 0, 0];
    for (let i = 0; i < 4; i++)
      o[i] = m[0 * 4 + i] * v[0] + m[1 * 4 + i] * v[1]
           + m[2 * 4 + i] * v[2] + m[3 * 4 + i] * v[3];
    const w = o[3] || 1;
    return [o[0] / w, o[1] / w, o[2] / w];
  }

  function inverter(m) {
    const a = m, o = new Float32Array(16);
    const s = [
      a[0] * a[5] - a[1] * a[4], a[0] * a[6] - a[2] * a[4], a[0] * a[7] - a[3] * a[4],
      a[1] * a[6] - a[2] * a[5], a[1] * a[7] - a[3] * a[5], a[2] * a[7] - a[3] * a[6]
    ];
    const c = [
      a[8] * a[13] - a[9] * a[12], a[8] * a[14] - a[10] * a[12], a[8] * a[15] - a[11] * a[12],
      a[9] * a[14] - a[10] * a[13], a[9] * a[15] - a[11] * a[13], a[10] * a[15] - a[11] * a[14]
    ];
    const det = s[0] * c[5] - s[1] * c[4] + s[2] * c[3] + s[3] * c[2] - s[4] * c[1] + s[5] * c[0];
    if (!det) return null;
    const d = 1 / det;

    o[0] = (a[5] * c[5] - a[6] * c[4] + a[7] * c[3]) * d;
    o[1] = (-a[1] * c[5] + a[2] * c[4] - a[3] * c[3]) * d;
    o[2] = (a[13] * s[5] - a[14] * s[4] + a[15] * s[3]) * d;
    o[3] = (-a[9] * s[5] + a[10] * s[4] - a[11] * s[3]) * d;
    o[4] = (-a[4] * c[5] + a[6] * c[2] - a[7] * c[1]) * d;
    o[5] = (a[0] * c[5] - a[2] * c[2] + a[3] * c[1]) * d;
    o[6] = (-a[12] * s[5] + a[14] * s[2] - a[15] * s[1]) * d;
    o[7] = (a[8] * s[5] - a[10] * s[2] + a[11] * s[1]) * d;
    o[8] = (a[4] * c[4] - a[5] * c[2] + a[7] * c[0]) * d;
    o[9] = (-a[0] * c[4] + a[1] * c[2] - a[3] * c[0]) * d;
    o[10] = (a[12] * s[4] - a[13] * s[2] + a[15] * s[0]) * d;
    o[11] = (-a[8] * s[4] + a[9] * s[2] - a[11] * s[0]) * d;
    o[12] = (-a[4] * c[3] + a[5] * c[1] - a[6] * c[0]) * d;
    o[13] = (a[0] * c[3] - a[1] * c[1] + a[2] * c[0]) * d;
    o[14] = (-a[12] * s[3] + a[13] * s[1] - a[14] * s[0]) * d;
    o[15] = (a[8] * s[3] - a[9] * s[1] + a[10] * s[0]) * d;
    return o;
  }

  // ------------------------------------------------------------------
  //  DESENHAR
  // ------------------------------------------------------------------

  function caixa(P, C, N, x, y, z, larg, alt, prof, cor) {
    for (const f of FACES) {
      const q = [f.v[0], f.v[1], f.v[2], f.v[0], f.v[2], f.v[3]];
      for (const v of q) {
        P.push(x + v[0] * larg, y + v[1] * alt, z + v[2] * prof);
        C.push(cor[0], cor[1], cor[2]);
        N.push(f.n[0], f.n[1], f.n[2]);
      }
    }
  }

  function desenhar(cam, jogador, alvo, inimigos) {
    const l = canvas.clientWidth, a = canvas.clientHeight;
    if (canvas.width !== l || canvas.height !== a) {
      canvas.width = l; canvas.height = a;
    }
    gl.viewport(0, 0, canvas.width, canvas.height);
    gl.clearColor(visual.ceu[0], visual.ceu[1], visual.ceu[2], 1);
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

    const { mvp } = matrizes(cam);
    gl.uniformMatrix4fv(gl.getUniformLocation(prog, 'uMVP'), false, mvp);

    // 1. o mundo
    ligar(bufPos, 'aPos'); ligar(bufCor, 'aCor'); ligar(bufNorm, 'aNorm');
    gl.drawArrays(gl.TRIANGLES, 0, verts);

    // 2. o jogador e a moldura do bloco mirado, numa malha temporaria
    const P = [], C = [], N = [];

    // O jogador, com as cores que ele escolheu no MinhaConfig.cs.
    if (jogador) {
      Bichos.desenhar(caixa, hexParaRgb, P, C, N, 'boneco',
                      jogador.x, jogador.y, jogador.z, {
        c: rgbParaHex(visual.calca), r: rgbParaHex(visual.roupa),
        a: rgbParaHex(visual.pele), p: rgbParaHex(visual.pele),
        o: '#241633', h: rgbParaHex(visual.cabelo)
      });
    }

    // Os monstros que o aluno criou no desafio 7.
    if (inimigos) {
      for (const m of inimigos) {
        Bichos.desenhar(caixa, hexParaRgb, P, C, N, m.desenho, m.x, m.y, m.z);
      }
    }

    if (alvo) {
      const e = 0.045;
      caixa(P, C, N, alvo.x - e, alvo.y - e, alvo.z - e,
            1 + 2 * e, 1 + 2 * e, 1 + 2 * e, [1.0, 1.0, 0.35]);
    }

    if (P.length) {
      const tmpP = gl.createBuffer(), tmpC = gl.createBuffer(), tmpN = gl.createBuffer();
      enviar(tmpP, P); enviar(tmpC, C); enviar(tmpN, N);
      ligar(tmpP, 'aPos'); ligar(tmpC, 'aCor'); ligar(tmpN, 'aNorm');

      if (alvo) {
        // A moldura amarela e desenhada por dentro: assim ela envolve o
        // cubo mirado sem tampar o que esta na frente.
        gl.cullFace(gl.FRONT);
        gl.drawArrays(gl.TRIANGLES, P.length / 3 - 36, 36);
        gl.cullFace(gl.BACK);
      }
      // A moldura amarela e a ultima caixa da malha; tudo antes dela
      // sao os bichos. Contar assim dispensa saber quantos cubinhos
      // cada monstro tem.
      const daMoldura = alvo ? 36 : 0;
      gl.drawArrays(gl.TRIANGLES, 0, P.length / 3 - daMoldura);

      gl.deleteBuffer(tmpP); gl.deleteBuffer(tmpC); gl.deleteBuffer(tmpN);
    }
  }

  return {
    iniciar: iniciar,
    carregarMundo: carregarMundo,
    remontar: remontar,
    mudar: mudar,
    mirar: mirar,
    desenhar: desenhar,
    seguirJogador: seguirJogador
  };
})();
