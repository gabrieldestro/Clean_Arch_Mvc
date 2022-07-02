using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchMVC.Domain.Tests
{
	public class ProductUnitTest1
	{
		// nao vou criar todos os metodos tendo em vista que a ideia eh apanas aprender o conceito.
		[Fact(DisplayName = "Create Product with valid state")]
		public void CreateProduct_WithValidParameters_ResultObjectValidState()
		{
			Action action = () => new Product(1, "Product", "Description", 9.99m, 9, "image");
			action.Should().NotThrow<DomainExceptionValidation>();
		}

		[Fact(DisplayName = "Create Product with negative Id")]
		public void CreateProduct_WithNegativeIdValue_ResultDomainExceptionInvalidId()
		{
			Action action = () => new Product(-1, "Product", "Description", 9.99m, 9, "image");
			action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid ID: cannot be lesser than 0.");
		}

		[Fact(DisplayName = "Create Product with null image")]
		public void CreateProduct_WithNullImage_ResulNullReferenceException()
		{
			Action action = () => new Product(1, "Product", "Description", 9.99m, 9, null);
			action.Should().NotThrow<NullReferenceException>();
		}

		[Theory(DisplayName = "Create Product with negative stock")]
		[InlineData(-5)]
		public void CreateProduct_WithNegativeStockValue_ResultDomainExceptionInvalidStock(int value)
		{
			Action action = () => new Product(1, "Product", "Description", 9.99m, value, "image");
			action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock: Stock lesser than 0.");
		}

		// Aqui deveria ficar o resto dos testes caso eu fosse implementa-los
	}
}
