using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
	/*
	 * Em um modelo de dominio real, aqui estariam todoas as regras de negocio da aplicacao
	 */
	public sealed class Product : Entity
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }
		public int Stock { get; private set; }
		public string Image { get; private set; }

		// propriedade de navegacao do EF, nao eh necessario private set nelas
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public Product(string name, string description, decimal price, int stock, string image)
		{
			validateDomain(name, description, price, stock, image);
			Name = name;
			Description = description;
			Price = price;
			Stock = stock;
			Image = image;
		}

		public Product(int id, string name, string description, decimal price, int stock, string image)
		{
			DomainExceptionValidation.When(id < 0, "Invalid id: ID lesser than 0.");
			validateDomain(name, description, price, stock, image);

			Id = id;
			Name = name;
			Description = description;
			Price = price;
			Stock = stock;
			Image = image;
		}

		public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
		{
			validateDomain(name, description, price, stock, image);
			Name = name;
			Description = description;
			Price = price;
			Stock = stock;
			Image = image;

			CategoryId = categoryId;
		}

		private void validateDomain(string name, string description, decimal price, int stock, string image)
		{
			DomainExceptionValidation.When(String.IsNullOrEmpty(name), "Invalid name: Name cannot be empty or null.");
			DomainExceptionValidation.When(name.Length < 3, "Invalid name: Name is too short.");

			DomainExceptionValidation.When(String.IsNullOrEmpty(description), "Invalid description: Description cannot be empty or null.");
			DomainExceptionValidation.When(description.Length < 5, "Invalid description: Description is too short.");
			
			DomainExceptionValidation.When(price < 0, "Invalid price: Price lesser than 0.");
			DomainExceptionValidation.When(stock < 0, "Invalid stock: Stock lesser than 0.");

			DomainExceptionValidation.When(image?.Length > 250, "Invalid image: Image is too big.");
		}
	}
}
