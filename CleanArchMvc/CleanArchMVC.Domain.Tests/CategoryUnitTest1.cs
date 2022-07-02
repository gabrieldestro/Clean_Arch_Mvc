using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMVC.Domain.Tests
{
	public class CategoryUnitTest1
	{
		[Fact(DisplayName = "Create category with valid state")]
		public void CreateCategory_WithValidParameters_ResultObjectValidState()
		{
			Action action = () => new Category(1, "Category");
			action.Should().NotThrow<DomainExceptionValidation>();
		}

		[Fact(DisplayName = "Create category with negative Id")]
		public void CreateCategory_WithNegativeIdValue_ResultDomainExceptionInvalidId()
		{
			Action action = () => new Category(-1, "Category");
			action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid ID: cannot be lesser than 0.");
		}

		[Fact(DisplayName = "Create category with short name")]
		public void CreateCategory_WithShortName_ResultDomainExceptionShortName()
		{
			Action action = () => new Category(0, "Ca");
			action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name: Name is too short.");
		}

		[Fact(DisplayName = "Create category with null name")]
		public void CreateCategory_WithNullName_ResultDomainExceptionNullName()
		{
			Action action = () => new Category(0, null);
			action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name: Name cannot be empty or null.");
		}
	}
}
