using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfCrud.Models;
using WpfCrud.ViewModels;

namespace WpfCrud.Tests
{
    [TestClass]
    public class EditProductViewModelUnitTests
    {
        [TestMethod]
        public void AssertConstructorThrowsWhenEditProductViewModelParamIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new EditProductViewModel(null));
        }

        [TestMethod]
        public void AssertValidatePropertiesThrowsWhenNamePropertyIsNullOrWhiteSpace()
        {
            const string expectedErrorMessage = "Наименование должно быть заполнено!";
            var editableProduct = new EditableProduct();
            var editProductVm = new EditProductViewModel(editableProduct);

            Action validatePropertiesMethodCall = () => editProductVm.ValidateProperties();

            editProductVm.Name = null;
            var e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.Name = string.Empty;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.Name = " ";
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }

        [TestMethod]
        public void AssertValidatePropertiesThrowsWhenCaloricContentPer100GramsIsNotGreaterThanZero()
        {
            const string expectedErrorMessage = "Калорийность (ккал / 100 г) должна быть положительным числом!";
            var editableProduct = new EditableProduct();
            var editProductVm = new EditProductViewModel(editableProduct);
            editProductVm.Name = "TEST";

            Action validatePropertiesMethodCall = () => editProductVm.ValidateProperties();

            editProductVm.CaloricContentPer100Grams = 0;
            var e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.CaloricContentPer100Grams = -1;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.CaloricContentPer100Grams = double.NegativeInfinity;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.CaloricContentPer100Grams = double.NaN;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }

        [TestMethod]
        public void AssertValidatePropertiesThrowsWhenWeightGramsIsNotGreaterThanZero()
        {
            const string expectedErrorMessage = "Масса (г) должна быть положительным числом!";
            var editableProduct = new EditableProduct();
            var editProductVm = new EditProductViewModel(editableProduct);
            editProductVm.Name = "TEST";
            editProductVm.CaloricContentPer100Grams = 1;

            Action validatePropertiesMethodCall = () => editProductVm.ValidateProperties();

            editProductVm.WeightGrams = 0;
            var e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.WeightGrams = -1;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.WeightGrams = double.NegativeInfinity;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.WeightGrams = double.NaN;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }

        [TestMethod]
        public void AssertValidatePropertiesThrowsWhenPricePerKilogramRoublesIsNotGreaterThanZero()
        {
            const string expectedErrorMessage = "Цена за 1 кг должна быть положительным числом!";
            var editableProduct = new EditableProduct();
            var editProductVm = new EditProductViewModel(editableProduct);
            editProductVm.Name = "TEST";
            editProductVm.CaloricContentPer100Grams = 1;
            editProductVm.WeightGrams = 1;

            Action validatePropertiesMethodCall = () => editProductVm.ValidateProperties();
            
            editProductVm.PricePerKilogramRoubles = 0;
            var e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.PricePerKilogramRoubles = -1;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);

            editProductVm.PricePerKilogramRoubles = decimal.MinValue;
            e = Assert.ThrowsException<Exception>(validatePropertiesMethodCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }
    }
}
