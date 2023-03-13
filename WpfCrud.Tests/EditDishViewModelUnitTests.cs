using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfCrud.Models;
using WpfCrud.ViewModels;

namespace WpfCrud.Tests
{
    [TestClass]
    public class EditDishViewModelUnitTests
    {
        private static readonly List<DishType> s_dishTypes = new List<DishType>
        {
            new DishType(1, "A"),
            new DishType(2, "B"),
            new DishType(3, "C"),
            new DishType(4, "D"),
            new DishType(5, "E"),
        };

        [TestMethod]
        public void AssertConstructorThrowsWhenEditableDishParamIsNull()
        {
            EditableDish editableDish = null;
            Assert
                .ThrowsException<ArgumentNullException>(
                    () => new EditDishViewModel(editableDish, s_dishTypes));
        }

        [TestMethod]
        public void AssertConstructorThrowsWhenDishTypesParamIsNull()
        {
            var editableDish = new EditableDish();
            List<DishType> dishTypes = null;
            Assert.ThrowsException<ArgumentNullException>(
                () => new EditDishViewModel(editableDish, dishTypes));
        }

        [TestMethod]
        public void AssertConstructorThrowsWhenDishTypesParamIsEmpty()
        {
            const string expectedErrorMessage = "Список типов блюд <dishTypes> пуст!";
            var et = new EditableDish();
            var dishTypes = Enumerable.Empty<DishType>();
            var e = Assert.ThrowsException<InvalidOperationException>(
                () => new EditDishViewModel(et, dishTypes));
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }

        [TestMethod]
        public void AssertValidatePropertiesThrowsWhenNameIsNullOrWhiteSpace()
        {
            const string expectedErrorMessage = "Название блюда должно быть заполнено!";
            var et = new EditableDish();
            var etvm = new EditDishViewModel(et, s_dishTypes);

            Action validateCall = () => etvm.ValidateProperties();

            Exception e = null;

            etvm.Name = null;
            e = Assert.ThrowsException<Exception>(validateCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);;

            etvm.Name = string.Empty;
            e = Assert.ThrowsException<Exception>(validateCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);;

            etvm.Name = " ";
            e = Assert.ThrowsException<Exception>(validateCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }

        [TestMethod]
        public void AssertValidatePropertiesThrowsWhenDishTypeIsNotSet()
        {
            const string expectedErrorMessage = "Тип блюда должен быть выбран!";
            var et = new EditableDish();
            var etvm = new EditDishViewModel(et, s_dishTypes);
            etvm.Name = "TEST";

            Action validateCall = () => etvm.ValidateProperties();

            Exception e = null;

            etvm.DishTypeId = 0;
            e = Assert.ThrowsException<Exception>(validateCall);
            Assert.AreEqual(expectedErrorMessage, e.Message);
        }
    }
}
