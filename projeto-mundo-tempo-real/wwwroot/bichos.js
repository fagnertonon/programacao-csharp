// ====================================================================
//  OS BICHOS - o jogador e os monstros.
//
//  Voce NAO precisa mexer aqui.
//
//  Um bicho e um DESENHO EM CAMADAS. Cada camada e uma fatia
//  horizontal, de baixo para cima; cada letra e um cubinho colorido, e
//  o ponto e vazio. E exatamente o mesmo formato que voce usa no
//  MeuMonstro.cs - a diferenca e que estes quatro ja vem prontos.
// ====================================================================

const Bichos = (function () {

  const prontos = {

    boneco: {
      escala: 0.145,
      cores: { c: '#2B3A6B', r: '#C43B4A', a: '#EBC29A', p: '#EBC29A',
               o: '#241633', h: '#5C3A26' },
      andares: [
        ['......', '.c..c.', '.c..c.', '......'],   // pes
        ['......', '.c..c.', '.c..c.', '......'],
        ['......', '.c..c.', '.c..c.', '......'],   // pernas
        ['......', '.rrrr.', '.rrrr.', '......'],   // quadril
        ['......', 'arrrra', 'arrrra', '......'],   // tronco e bracos
        ['......', 'arrrra', 'arrrra', '......'],
        ['......', '.rrrr.', '.rrrr.', '......'],   // ombros
        ['......', '.pppp.', '.pppp.', '......'],   // pescoco
        ['......', '.oppo.', '.pppp.', '......'],   // rosto
        ['......', '.pppp.', '.pppp.', '......'],
        ['......', '.hhhh.', '.hhhh.', '......']    // cabelo
      ]
    },

    gosma: {
      escala: 0.15,
      cores: { v: '#4FD16A', o: '#0E2A16' },
      andares: [
        ['.vvvv.', 'vvvvvv', 'vvvvvv', 'vvvvvv', 'vvvvvv', '.vvvv.'],
        ['.vvvv.', 'vvvvvv', 'vvvvvv', 'vvvvvv', 'vvvvvv', '.vvvv.'],
        ['.ovvo.', 'vvvvvv', 'vvvvvv', 'vvvvvv', 'vvvvvv', '.vvvv.'],
        ['..vv..', '.vvvv.', '.vvvv.', '.vvvv.', '.vvvv.', '..vv..'],
        ['......', '..vv..', '.vvvv.', '.vvvv.', '..vv..', '......']
      ]
    },

    fantasma: {
      escala: 0.15,
      cores: { f: '#F2F0FA', o: '#3A2B55' },
      andares: [
        ['f.f.f.', '.f.f.f', 'f.f.f.', '.f.f.f', 'f.f.f.', '.f.f.f'],
        ['.ffff.', 'ffffff', 'ffffff', 'ffffff', 'ffffff', '.ffff.'],
        ['.ffff.', 'ffffff', 'ffffff', 'ffffff', 'ffffff', '.ffff.'],
        ['.offo.', 'ffffff', 'ffffff', 'ffffff', 'ffffff', '.ffff.'],
        ['..ff..', '.ffff.', '.ffff.', '.ffff.', '.ffff.', '..ff..'],
        ['......', '..ff..', '.ffff.', '.ffff.', '..ff..', '......']
      ]
    },

    aranha: {
      escala: 0.14,
      cores: { p: '#2A2233', o: '#E24A4A', c: '#4A3D57' },
      andares: [
        ['c....c', '.c..c.', '......', '......', '.c..c.', 'c....c'],
        ['.c..c.', '..pp..', '.pppp.', '.pppp.', '..pp..', '.c..c.'],
        ['......', '.pppp.', 'pppppp', 'pppppp', '.pppp.', '......'],
        ['......', '..pp..', '.oppo.', '.pppp.', '..pp..', '......']
      ]
    },

    robo: {
      escala: 0.15,
      cores: { m: '#8A94A6', o: '#39FF14', a: '#C8542F', e: '#5A6478' },
      andares: [
        ['......', '.e..e.', '.e..e.', '......'],
        ['......', '.mmmm.', '.mmmm.', '......'],
        ['......', 'ammmma', 'ammmma', '......'],
        ['......', 'ammmma', 'ammmma', '......'],
        ['......', '.mmmm.', '.mmmm.', '......'],
        ['......', '.ommo.', '.mmmm.', '......'],
        ['..mm..', '.mmmm.', '.mmmm.', '..mm..']
      ]
    }
  };

  // O monstro do aluno chega do servidor, ja peneirado pelo Desenhos.cs.
  let meu = null;

  function guardarMeuMonstro(m) {
    if (!m || !m.andares || !m.andares.length) { return; }
    meu = { escala: 0.15, cores: m.cores || {}, andares: m.andares, nome: m.nome };
  }

  function de(nome) {
    if (nome === 'meumonstro') { return meu || prontos.gosma; }
    return prontos[nome] || prontos.gosma;
  }

  function nomeDoMeu() {
    return (meu && meu.nome) ? meu.nome : 'meu monstro';
  }

  // Vira cubinhos. O 'caixa' e o 'hexParaRgb' vem do render.js: assim
  // este arquivo nao precisa saber nada de WebGL.
  function desenhar(caixa, hexParaRgb, P, C, N, nome, x, y, z, cores) {
    const b = de(nome);
    if (!b) { return; }

    const paleta = cores || b.cores;
    const e = b.escala;
    const altura = b.andares.length;
    const fundo = b.andares[0].length;
    const largura = b.andares[0][0].length;

    // Centraliza o bicho na casa de 1 x 1 em que ele esta.
    const ox = x + 0.5 - (largura * e) / 2;
    const oz = z + 0.5 - (fundo * e) / 2;

    for (let cy = 0; cy < altura; cy++) {
      const andar = b.andares[cy];
      for (let cz = 0; cz < andar.length; cz++) {
        const linha = andar[cz];
        for (let cx = 0; cx < linha.length; cx++) {
          const letra = linha[cx];
          if (!letra || letra === '.' || letra === ' ') { continue; }

          const hex = paleta[letra];
          if (!hex) { continue; }

          caixa(P, C, N,
                ox + cx * e, y + 0.02 + cy * e, oz + cz * e,
                e, e, e, hexParaRgb(hex));
        }
      }
    }
  }

  return {
    desenhar: desenhar,
    guardarMeuMonstro: guardarMeuMonstro,
    nomeDoMeu: nomeDoMeu
  };
})();
