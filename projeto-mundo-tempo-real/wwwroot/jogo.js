// ====================================================================
//  O JOGO: teclado, mouse, camera e conversa com o C#.
//
//  Voce NAO precisa mexer aqui.
//
//  A DIVISAO DE TRABALHO, que e a ideia inteira deste projeto:
//    - o NAVEGADOR desenha e escuta o teclado;
//    - o C# DECIDE tudo: se da para andar, ate onde sobe o pulo, o que
//      cai quando voce mina, se o bloco pode ser colocado.
//
//  Nenhuma regra do jogo mora aqui. Toda vez que voce aperta uma tecla,
//  a pergunta viaja para o servidor, o metodo que VOCE escreveu em C#
//  responde, e a tela so mostra o resultado.
// ====================================================================

(function () {
  const tela = document.getElementById('tela');

  let mundo = null;
  let jogador = { x: 0, y: 0, z: 0 };
  let destravado = {};
  let alvo = null, vizinho = null;
  let inimigos = [];
  let pegou = 0;
  let vida = 10, vidaCheia = 10;
  let blocoEscolhido = '';
  let ocupado = false;

  // A camera orbita em volta do jogador. O alvo dela e suavizado para o
  // movimento em degraus do jogo nao virar solavanco na tela.
  const cam = { alvo: [12, 10, 12], yaw: 0.8, pitch: 0.62, dist: 17 };
  let mouseX = 0, mouseY = 0;

  // ------------------------------------------------------------------
  //  CONVERSA COM O C#
  // ------------------------------------------------------------------

  async function pedir(rota, corpo) {
    const r = await fetch(rota, {
      method: corpo ? 'POST' : 'GET',
      headers: { 'Content-Type': 'application/json' },
      body: corpo ? JSON.stringify(corpo) : undefined
    });
    if (!r.ok) throw new Error(rota + ' respondeu ' + r.status);
    return r.json();
  }

  async function carregarMundo() {
    mundo = await pedir('/api/mundo');
    jogador = mundo.jogador;
    destravado = mundo.destravado;
    inimigos = mundo.inimigos || [];
    Render.carregarMundo(mundo);
    cam.alvo = [jogador.x + 0.5, jogador.y + 1, jogador.z + 0.5];
    montarPaleta();
    pintarComandos();
    if (typeof mundo.vida === 'number') {
      vida = mundo.vida; vidaCheia = mundo.vidaCheia || 10; pintarVida();
    }
    document.getElementById('semente').value = mundo.semente;

    // O nome que o aluno pos no MinhaConfig.cs vira o titulo do mundo.
    const c = mundo.config;
    if (c && c.jogador) {
      document.querySelector('#topo h1').textContent = 'O MUNDO DE ' + c.jogador.toUpperCase();
    }
  }

  async function carregarDesafios() {
    const d = await pedir('/api/desafios');
    Painel.montar(d);
    // Resolver o desafio 6 acrescenta tipos de bloco: o mundo precisa
    // ser relido para a paleta ganhar os blocos novos.
    await carregarMundo();
  }

  function aplicar(resposta) {
    jogador = resposta.jogador;
    destravado = resposta.destravado;

    if (resposta.inimigos) { inimigos = resposta.inimigos; }

    if (typeof resposta.vida === 'number') {
      if (resposta.vida < vida) { piscar(); }
      vida = resposta.vida;
      vidaCheia = resposta.vidaCheia || 10;
      pintarVida();
    }

    // Encostar nao mata ninguem: so conta e avisa. Quem quiser escapar
    // constroi uma parede - o monstro nao sobe degrau.
    if (typeof resposta.pegou === 'number' && resposta.pegou > pegou) {
      pegou = resposta.pegou;
      const p = document.getElementById('pegou');
      p.hidden = false;
      p.textContent = 'pego ' + pegou + 'x';
      avisar('O ' + nomeBonito(resposta.quemPegou) + ' te encostou!', 'ruim');
    }

    if (resposta.mudou && resposta.mudou.length) {
      resposta.mudou.forEach(c => Render.mudar(c.x, c.y, c.z, c.t));
      Render.remontar();
    }

    if (resposta.recado) { avisar(resposta.recado, 'ruim'); }
    else if (resposta.mensagem) { avisar(resposta.mensagem, 'bom'); }

    pintarComandos();
  }

  // ------------------------------------------------------------------
  //  RECADO
  // ------------------------------------------------------------------

  let tempoRecado = null;
  function avisar(texto, tom) {
    const el = document.getElementById('recado');
    el.textContent = texto;
    el.className = 'visivel ' + (tom || '');
    clearTimeout(tempoRecado);
    tempoRecado = setTimeout(() => { el.className = ''; }, 3200);
  }

  // ------------------------------------------------------------------
  //  PALETA
  // ------------------------------------------------------------------

  // O ar, o bedrock e a agua ficam de fora: um nao e bloco, o outro nao
  // quebra, e o terceiro nao faz sentido na mao.
  const FORA_DA_PALETA = ['ar', 'bedrock', 'agua'];

  const DE_FABRICA = ['pedra', 'terra', 'grama', 'areia', 'tronco', 'folha', 'cascalho'];

  // OS BLOCOS DO ALUNO VEM PRIMEIRO, e isto foi um defeito de verdade:
  // a paleta mostrava os sete de fabrica e cortava em nove, entao de dez
  // blocos inventados so dois apareciam. O aluno criava dez e usava dois,
  // sem entender por que.
  //
  // Agora os dele abrem a fila, as teclas 1 a 9 caem neles primeiro, e a
  // paleta quebra em duas linhas em vez de esconder o resto.
  function paletaOrdenada() {
    if (!mundo) { return []; }
    const usaveis = mundo.tipos.filter(t => FORA_DA_PALETA.indexOf(t.nome) < 0);
    const meus = usaveis.filter(t => DE_FABRICA.indexOf(t.nome) < 0);
    const casa = usaveis.filter(t => DE_FABRICA.indexOf(t.nome) >= 0);
    return meus.concat(casa);
  }

  function montarPaleta() {
    const el = document.getElementById('paleta');
    el.innerHTML = '';

    const lista = paletaOrdenada();

    if (!blocoEscolhido || !lista.some(t => t.nome === blocoEscolhido)) {
      blocoEscolhido = lista.length ? lista[0].nome : '';
    }

    lista.forEach((t, i) => {
      const meu = DE_FABRICA.indexOf(t.nome) < 0;
      const slot = document.createElement('div');
      slot.className = 'slot' + (t.nome === blocoEscolhido ? ' ativo' : '') + (meu ? ' meu' : '');
      slot.title = t.nome + ' - dureza ' + t.dureza + (meu ? ' (seu)' : '');
      slot.innerHTML =
        '<span class="tecla">' + (i < 9 ? (i + 1) : '&nbsp;') + '</span>' +
        '<span class="amostra" style="background:' + t.cor + '"></span>' +
        '<span class="nome">' + t.nome + '</span>';
      slot.onclick = () => { blocoEscolhido = t.nome; montarPaleta(); };
      el.appendChild(slot);
    });
  }

  function pintarVida() {
    const el = document.getElementById('vida');
    if (!el) { return; }
    let html = '';
    for (let i = 0; i < vidaCheia; i++) {
      html += '<i class="' + (i < vida ? '' : 'vazio') + '"></i>';
    }
    el.innerHTML = html;
  }

  let tempoTremor = null;
  function piscar() {
    document.body.classList.add('machucado');
    clearTimeout(tempoTremor);
    tempoTremor = setTimeout(() => document.body.classList.remove('machucado'), 300);
  }

  // O LACO DO TEMPO REAL.
  //
  // Na versao por turno isto nao existia: o mundo so mudava quando o
  // jogador apertava alguma coisa. Aqui os monstros tem relogio proprio,
  // entao a tela precisa perguntar "o que mudou?" o tempo todo.
  //
  // O intervalo de 180 ms nao e por acaso: o relogio do servidor bate a
  // cada 550 ms, entao perguntar tres vezes por batida garante que
  // nenhum passo dos monstros passe despercebido, sem inundar o servidor.
  let perguntando = false;
  async function ouvirOMundo() {
    if (perguntando || ocupado) { return; }
    perguntando = true;
    try {
      const r = await pedir('/api/estado');
      jogador = r.jogador;
      destravado = r.destravado;
      if (r.inimigos) { inimigos = r.inimigos; }

      if (typeof r.vida === 'number') {
        if (r.vida < vida) { piscar(); }
        vida = r.vida; vidaCheia = r.vidaCheia || 10;
        pintarVida();
      }
      if (r.pegou > pegou) {
        pegou = r.pegou;
        const p = document.getElementById('pegou');
        p.hidden = false; p.textContent = 'pego ' + pegou + 'x';
      }
      if (r.recado) { avisar(r.recado, 'ruim'); }
    } catch (err) { /* servidor caiu: o proximo tique tenta de novo */ }
    finally { perguntando = false; }
  }

  setInterval(ouvirOMundo, 180);

  function nomeBonito(desenho) {
    if (desenho === 'meumonstro') { return Bichos.nomeDoMeu(); }
    return desenho || 'monstro';
  }

  function pintarComandos() {
    document.querySelectorAll('#comandos .linha[data-trava]').forEach(l => {
      l.classList.toggle('livre', !!destravado[l.dataset.trava]);
    });
  }

  // ------------------------------------------------------------------
  //  TECLADO
  // ------------------------------------------------------------------

  const teclas = {};
  let ultimoPasso = 0;

  addEventListener('keydown', async (e) => {
    const k = e.key.toLowerCase();
    teclas[k] = true;

    if (k >= '1' && k <= '9') {
      const lista = paletaOrdenada();
      const i = parseInt(k, 10) - 1;
      if (lista[i]) { blocoEscolhido = lista[i].nome; montarPaleta(); }
      return;
    }

    // O espaco vem como " " no key, mas ha teclado e automacao que mandam
    // o key vazio. O code e a tecla FISICA e nunca mente - por isso os dois.
    if (k === ' ' || e.code === 'Space')
    {
      e.preventDefault();
      await agir('/api/pular', { forca: 2 });
      return;
    }

    if (k === 'c') { await cavar(); }

    // O passo sai JA no keydown - veja a nota em darPasso.
    if (TECLAS_DE_ANDAR.indexOf(k) >= 0) {
      e.preventDefault();
      const [dx, dz] = direcaoDasTeclas();
      await darPasso(dx, dz, performance.now());
    }
  });

  addEventListener('keyup', (e) => { teclas[e.key.toLowerCase()] = false; });

  const TECLAS_DE_ANDAR = ['w', 'a', 's', 'd',
                          'arrowup', 'arrowdown', 'arrowleft', 'arrowright'];

  function direcaoDasTeclas() {
    let dx = 0, dz = 0;
    if (teclas['w'] || teclas['arrowup']) dz -= 1;
    if (teclas['s'] || teclas['arrowdown']) dz += 1;
    if (teclas['a'] || teclas['arrowleft']) dx -= 1;
    if (teclas['d'] || teclas['arrowright']) dx += 1;
    return [dx, dz];
  }

  // O passo e discreto - um cubo por vez -, entao nao adianta mandar 60
  // pedidos por segundo. O intervalo de 130 ms segura o ritmo de quem
  // fica com a tecla apertada.
  //
  // ATENCAO, e isto foi um defeito de verdade: este metodo NAO pode ser
  // chamado so pelo laco da tela. Um toque rapido em W solta a tecla
  // antes do proximo quadro, o laco le "nenhuma tecla apertada", e o
  // boneco nao anda - so andava para quem SEGURAVA a tecla. Por isso o
  // keydown chama isto na hora, e o laco cuida so da tecla segurada.
  async function darPasso(dx, dz, agora) {
    if (ocupado) return;
    if (!dx && !dz) return;
    if (agora - ultimoPasso < 130) return;

    // Andar segue a camera: W e sempre "para longe de quem olha".
    const c = Math.cos(cam.yaw), s = Math.sin(cam.yaw);
    const fx = Math.round(dz * s + dx * c);
    const fz = Math.round(dz * c - dx * s);

    ultimoPasso = agora;
    await agir('/api/andar', { dx: fx, dz: fz });
  }

  async function passo(agora) {
    const [dx, dz] = direcaoDasTeclas();
    await darPasso(dx, dz, agora);
  }

  async function agir(rota, corpo) {
    if (ocupado) return;
    ocupado = true;
    try { aplicar(await pedir(rota, corpo)); }
    catch (err) { avisar('O servidor nao respondeu: ' + err.message, 'ruim'); }
    finally { ocupado = false; }
  }

  async function cavar() {
    const onde = alvo || { x: jogador.x, y: jogador.y - 1, z: jogador.z };
    await agir('/api/cavar', { x: onde.x, y: onde.y, z: onde.z, profundidade: 4 });
  }

  // ------------------------------------------------------------------
  //  MOUSE
  // ------------------------------------------------------------------

  let arrastando = false, ax = 0, ay = 0, moveu = 0;

  // ARRASTAR gira a camera. CLICAR age no bloco. O limiar de 4 pixels
  // separa as duas coisas - sem ele, todo giro terminaria minerando.
  //
  // ATENCAO, e isto foi um defeito de verdade: as acoes NAO podem depender
  // de o evento ter aterrissado NO canvas. Ha navegador e automacao que
  // entregam o clique so na janela, e ai o botao direito nunca colocava
  // bloco. Os ouvintes ficam na JANELA, e quem decide se o clique foi no
  // mundo ou num botao da tela e o elementFromPoint - que pergunta "quem
  // esta por cima neste pixel?" e responde certo em qualquer navegador.
  function noMundo(e) {
    return document.elementFromPoint(e.clientX, e.clientY) === tela;
  }

  addEventListener('mousedown', (e) => {
    if (!noMundo(e)) { arrastando = false; return; }
    arrastando = true; moveu = 0; ax = e.clientX; ay = e.clientY;
  });

  addEventListener('mouseup', () => { arrastando = false; });

  addEventListener('mousemove', (e) => {
    mouseX = e.clientX; mouseY = e.clientY;
    if (!arrastando) return;

    const dx = e.clientX - ax, dy = e.clientY - ay;
    moveu += Math.abs(dx) + Math.abs(dy);
    ax = e.clientX; ay = e.clientY;

    cam.yaw -= dx * 0.006;
    // O teto de 1.05 (uns 60 graus) segura a camera num angulo de jogo.
    // Com 1.45 ela apontava quase para baixo, via so o alto da cabeca do
    // boneco e entrava em qualquer copa de arvore.
    cam.pitch = Math.max(0.10, Math.min(0.95, cam.pitch + dy * 0.005));
  });

  addEventListener('click', async (e) => {
    if (!noMundo(e) || moveu > 4) return;
    if (!alvo) { avisar('Mire num bloco.', 'ruim'); return; }
    await agir('/api/minerar', { x: alvo.x, y: alvo.y, z: alvo.z, picareta: 3 });
  });

  addEventListener('contextmenu', (e) => {
    if (!noMundo(e)) { return; }
    e.preventDefault();
    if (moveu > 4) { return; }
    if (!vizinho) { avisar('Nao ha espaco livre na mira.', 'ruim'); return; }
    agir('/api/colocar', {
      x: vizinho.x, y: vizinho.y, z: vizinho.z, tipo: blocoEscolhido
    });
  });

  addEventListener('wheel', (e) => {
    if (!noMundo(e)) { return; }
    e.preventDefault();
    cam.dist = Math.max(6, Math.min(40, cam.dist + Math.sign(e.deltaY) * 1.6));
  }, { passive: false });



  // ------------------------------------------------------------------
  //  BOTOES
  // ------------------------------------------------------------------

  function sincronizarPainel() {
    const aberto = document.getElementById('painel').classList.contains('aberto');
    document.body.classList.toggle('com-painel', aberto);
  }

  document.getElementById('btnPainel').onclick = () => {
    document.getElementById('painel').classList.toggle('aberto');
    sincronizarPainel();
  };

  sincronizarPainel();

  document.getElementById('btnConferir').onclick = async () => {
    document.getElementById('avisoBuild').textContent = 'conferindo...';
    try {
      await carregarDesafios();
      document.getElementById('avisoBuild').textContent =
        'O Desafios.cs e COMPILADO: para o resultado mudar, pare o programa e rode de novo com F5.';
    } catch (err) {
      document.getElementById('avisoBuild').textContent = 'Erro: ' + err.message;
    }
  };

  document.getElementById('btnReiniciar').onclick = async () => {
    const s = parseInt(document.getElementById('semente').value, 10) || 7;
    try {
      const d = await pedir('/api/reiniciar', { semente: s });
      mundo = d; jogador = d.jogador; destravado = d.destravado;
      inimigos = d.inimigos || [];
      pegou = 0; document.getElementById('pegou').hidden = true;
      Render.carregarMundo(mundo);
      cam.alvo = [jogador.x + 0.5, jogador.y + 1, jogador.z + 0.5];
      montarPaleta(); pintarComandos();
      avisar('Mundo novo com a semente ' + s + '.', 'bom');
    } catch (err) { avisar('Nao consegui criar o mundo: ' + err.message, 'ruim'); }
  };

  // ------------------------------------------------------------------
  //  O LACO DA TELA
  // ------------------------------------------------------------------

  // Uma janelinha para o professor espiar o estado do jogo no console do
  // navegador (F12), sem precisar mexer no codigo:
  //     MundoDeCubos.estado()
  window.MundoDeCubos = {
    estado: () => ({
      jogador: jogador, alvo: alvo, vizinho: vizinho,
      blocoEscolhido: blocoEscolhido, destravado: destravado,
      arrastando: arrastando, moveu: moveu, ocupado: ocupado,
      mouse: [mouseX, mouseY], camera: { yaw: cam.yaw, pitch: cam.pitch, dist: cam.dist }
    })
  };

  function quadro(agora) {
    // A camera persegue o jogador com atraso: e o que transforma o
    // passo em degraus num deslize suave.
    const meta = [jogador.x + 0.5, jogador.y + 1, jogador.z + 0.5];
    for (let i = 0; i < 3; i++) cam.alvo[i] += (meta[i] - cam.alvo[i]) * 0.18;

    Render.seguirJogador(jogador);

    const mira = Render.mirar(cam, mouseX, mouseY);
    alvo = mira ? mira.alvo : null;
    vizinho = mira ? mira.vizinho : null;

    Render.desenhar(cam, jogador, alvo, inimigos);
    passo(agora);
    requestAnimationFrame(quadro);
  }

  // ------------------------------------------------------------------

  (async function comecar() {
    try {
      Render.iniciar(tela);
      await carregarMundo();
      await carregarDesafios();
      mouseX = innerWidth / 2; mouseY = innerHeight / 2;
      requestAnimationFrame(quadro);
    } catch (err) {
      document.getElementById('pane').classList.remove('escondido');
      document.getElementById('paneTexto').textContent =
        err.message + '\n\nSe a mensagem fala em WebGL, o navegador desta '
        + 'maquina nao tem aceleracao 3D ligada. Tente o Chrome ou o Edge.';
    }
  })();
})();
