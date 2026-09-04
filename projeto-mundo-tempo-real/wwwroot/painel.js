// ====================================================================
//  O PAINEL DOS DESAFIOS. Voce NAO precisa mexer aqui.
//
//  Ele pergunta ao servidor quais testes passaram e pinta os cartoes.
//  Quem roda os testes e o C#, no Corretor.cs - isto aqui so mostra.
// ====================================================================

const Painel = (function () {
  let estado = null;

  function montar(dados) {
    estado = dados;
    const lista = document.getElementById('listaDesafios');
    lista.innerHTML = '';

    let feitos = 0;
    let liberado = true;   // o desafio 1 nasce aberto

    dados.desafios.forEach((d, i) => {
      const r = dados.resultados[i] || { passaram: 0, total: 0, resolvido: false, testes: [] };
      if (r.resolvido) feitos++;

      const cartao = document.createElement('div');
      cartao.className = 'cartao ' + (r.resolvido ? 'feito' : (liberado ? 'aberto' : 'travado'));

      const topo = document.createElement('div');
      topo.className = 'cartao-topo';
      topo.innerHTML =
        '<span class="numero">' + d.numero + '</span>' +
        '<h3>' + escapar(d.titulo) + '</h3>' +
        '<span class="revisa">' + escapar(d.revisa) + '</span>';
      cartao.appendChild(topo);

      const destrava = document.createElement('p');
      destrava.className = 'destrava';
      destrava.textContent = (r.resolvido ? 'LIGADO: ' : 'destrava: ') + d.destrava;
      cartao.appendChild(destrava);

      const selo = document.createElement('div');
      if (r.resolvido) {
        selo.className = 'selo ok';
        selo.textContent = 'RESOLVIDO - ' + r.total + ' de ' + r.total + ' testes';
      } else {
        selo.className = 'selo parcial';
        // Cuidado ao ler este numero: um metodo ainda vazio devolve o
        // valor padrao do tipo e ja acerta algum teste por acidente.
        selo.textContent = r.passaram + ' de ' + r.total + ' testes passando';
      }
      cartao.appendChild(selo);

      const assin = document.createElement('div');
      assin.className = 'assinatura';
      assin.textContent = d.assinatura;
      cartao.appendChild(assin);

      // O enunciado e a explicacao ficam escondidos ate alguem pedir:
      // com seis cartoes abertos o painel viraria um paredao de texto.
      const abrir = document.createElement('button');
      abrir.className = 'ligar';
      abrir.textContent = 'ver o enunciado, a explicacao e a dica';
      const detalhe = document.createElement('div');
      detalhe.style.display = 'none';
      abrir.onclick = () => {
        const mostrando = detalhe.style.display !== 'none';
        detalhe.style.display = mostrando ? 'none' : 'block';
        abrir.textContent = mostrando
          ? 'ver o enunciado, a explicacao e a dica'
          : 'esconder';
      };

      let html = '<p class="destrava"><b>' + escapar(d.enunciado) + '</b></p>';
      (d.explicacao || []).forEach(p => {
        html += '<p class="destrava">' + escapar(p) + '</p>';
      });
      html += '<p class="destrava" style="color:var(--ambar)"><b>Dica:</b> '
            + escapar(d.dica) + '</p>';
      detalhe.innerHTML = html;

      cartao.appendChild(abrir);
      cartao.appendChild(detalhe);

      // Os testes: os que falharam sempre a vista, com esperado e obtido.
      const testes = document.createElement('div');
      testes.className = 'testes';
      (r.testes || []).forEach(t => {
        const linha = document.createElement('div');
        linha.className = 'teste ' + (t.passou ? 'passou' : 'falhou');
        let corpo = '<span class="marca">' + (t.passou ? 'OK' : 'X') + '</span><span>'
                  + escapar(t.descricao);
        if (!t.passou) {
          corpo += '<span class="detalhe">esperado: [' + escapar(t.esperado) + ']</span>'
                 + '<span class="detalhe">obtido:&nbsp;&nbsp; [' + escapar(t.obtido) + ']</span>';
          if (t.erro) corpo += '<span class="nota">' + escapar(t.erro) + '</span>';
        }
        linha.innerHTML = corpo + '</span>';
        testes.appendChild(linha);
      });
      cartao.appendChild(testes);

      lista.appendChild(cartao);

      // Destravamento em cadeia: o proximo so abre quando este resolve.
      liberado = r.resolvido;
    });

    document.getElementById('placar').textContent = feitos + ' / ' + dados.desafios.length;
    document.getElementById('tituloPainel').textContent =
      'Os ' + dados.desafios.length + ' desafios';
    document.getElementById('subtitulo').textContent = dados.subtitulo || '';

    return feitos;
  }

  function escapar(t) {
    return String(t == null ? '' : t)
      .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
  }

  return { montar: montar, estado: () => estado };
})();
