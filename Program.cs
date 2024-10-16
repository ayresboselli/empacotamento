using Empacotamento;
using Empacotamento.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/empacotamento", (Entrada body) =>
{
    var saida = new Saida();
    
    for(int i = 0; i < body.pedidos.Count; i++)
    {
        var pedido = body.pedidos[i];

        // ordena a lista de produtos em ordem decrescente
        pedido.produtos?.OrderByDescending(p => p.dimensoes?.Cubagem);

        saida.pedidos.Add(new PedidoSaida {
            pedido_id = pedido.pedido_id,
            caixas = EmpacotaProdutos.Empacotar(pedido)
        });
    }

    return saida;
})
.WithName("Empacotamento")
.WithOpenApi();


app.Run();
