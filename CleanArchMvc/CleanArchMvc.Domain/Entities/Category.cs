using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
	public sealed class Category : Entity
	{
		public string Name { get; private set; }

		// propriedade de navegacao do EF, nao eh necessario private set nelas
		public ICollection<Product> Products { get; set; }

		public Category(string name)
		{
			validateDomain(name);
			Name = name;
		}

		public Category(int id, string name)
		{
			DomainExceptionValidation.When(id < 0, "Invalid ID: cannot be lesser than 0.");
			validateDomain(name);

			Id = id;
			Name = name;
		}
		public void UpdateName(string name)
		{
			validateDomain(name);
			Name = name;
		}

		private void validateDomain(string name)
		{
			DomainExceptionValidation.When(String.IsNullOrEmpty(name), "Invalid name: Name cannot be empty or null.");
			DomainExceptionValidation.When(name.Length < 3, "Invalid name: Name is too short.");
		}
	}
}
