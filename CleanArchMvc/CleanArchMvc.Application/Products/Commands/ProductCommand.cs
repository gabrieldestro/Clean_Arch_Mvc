using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Commands
{
    
    // nao eh obrigado a criar a classe base
    // podemos ter mais propriedades para casos em que existam mais entidades relacionadas ao comando
    public abstract class ProductCommand : IRequest<Product>
    {
        
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}