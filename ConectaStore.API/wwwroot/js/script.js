// Referência ao container principal
const appContent = document.getElementById('app-content');

// Banco de dados simulado no LocalStorage
let produtos = JSON.parse(localStorage.getItem('produtos')) || [];
let categorias = JSON.parse(localStorage.getItem('categorias')) || ['Geral'];

// Definição das Views (Páginas)
const views = {
    // 1. Página Inicial (Interface Principal)
    home: () => `
        <h2>Bem-vindo à Interface Principal</h2>
        <p>Utilize o menu superior para navegar pelo sistema.</p>
        <div class="card-grid">
            <div class="card">
                <h3>Total de Produtos</h3>
                <p>${produtos.length}</p>
            </div>
        </div>
    `,

    // 2. Página de Produtos
    produtos: () => `
        <h2>Nossos Produtos</h2>
        <div class="card-grid">
            ${produtos.length > 0 ? produtos.map((p, index) => `
                <div class="card">
                    <h3>${p.nome}</h3>
                    <p>R$ ${p.preco}</p>
                    <button class="btn" onclick="navegarPara('produtoDetalhe', ${index})">Ver Detalhes</button>
                </div>
            `).join('') : '<p>Nenhum produto cadastrado ainda.</p>'}
        </div>
    `,

    // 3. Página do Produto Clicado
    produtoDetalhe: (index) => {
        const produto = produtos[index];
        if(!produto) return `<p>Produto não encontrado.</p>`;
        return `
            <h2>${produto.nome}</h2>
            <p><strong>Categoria:</strong> ${produto.categoria}</p>
            <p><strong>Preço:</strong> R$ ${produto.preco}</p>
            <p><strong>Descrição:</strong> ${produto.descricao || 'Sem descrição'}</p>
            <button class="btn" onclick="navegarPara('produtos')">Voltar</button>
        `;
    },

    // 4. Página Administrativa
    admin: () => `
        <h2>Painel Administrativo</h2>
        <div class="admin-menu">
            <button class="btn" onclick="navegarPara('crudCategoria')">Gerenciar Categorias</button>
            <button class="btn" onclick="navegarPara('crudProduto')">Gerenciar Produtos</button>
        </div>
        <p>Selecione uma opção acima para realizar cadastros e edições.</p>
    `,

    // 5. Página CRUD de Categoria
    crudCategoria: () => `
        <h2>Cadastro de Categorias</h2>
        <button class="btn" style="background: gray;" onclick="navegarPara('admin')">Voltar ao Painel</button>
        <form onsubmit="salvarCategoria(event)">
            <label>Nova Categoria:</label>
            <input type="text" id="cat-nome" required>
            <button class="btn" type="submit">Salvar Categoria</button>
        </form>
        <h3>Categorias Existentes:</h3>
        <ul>
            ${categorias.map(c => `<li>${c}</li>`).join('')}
        </ul>
    `,

    // 6. Página CRUD de Produto
    crudProduto: () => `
        <h2>Cadastro de Produtos</h2>
        <button class="btn" style="background: gray;" onclick="navegarPara('admin')">Voltar ao Painel</button>
        <form onsubmit="salvarProduto(event)">
            <input type="text" id="prod-nome" placeholder="Nome do Produto" required>
            <input type="number" step="0.01" id="prod-preco" placeholder="Preço" required>
            <select id="prod-categoria" required>
                ${categorias.map(c => `<option value="${c}">${c}</option>`).join('')}
            </select>
            <textarea id="prod-desc" placeholder="Descrição"></textarea>
            <button class="btn" type="submit">Salvar Produto</button>
        </form>
    `
};

// Função controladora de roteamento
function navegarPara(rota, parametro = null) {
    if (views[rota]) {
        appContent.innerHTML = views[rota](parametro);
    }
}

// Lógica de salvamento (CRUD)
function salvarCategoria(event) {
    event.preventDefault();
    const nome = document.getElementById('cat-nome').value;
    categorias.push(nome);
    localStorage.setItem('categorias', JSON.stringify(categorias));
    navegarPara('crudCategoria'); // Recarrega a view
}

function salvarProduto(event) {
    event.preventDefault();
    const produto = {
        nome: document.getElementById('prod-nome').value,
        preco: document.getElementById('prod-preco').value,
        categoria: document.getElementById('prod-categoria').value,
        descricao: document.getElementById('prod-desc').value
    };
    produtos.push(produto);
    localStorage.setItem('produtos', JSON.stringify(produtos));
    alert('Produto salvo com sucesso!');
    navegarPara('produtos'); // Redireciona para ver o catálogo
}

// Inicialização: Garante que o usuário caia direto na interface principal
navegarPara('home');