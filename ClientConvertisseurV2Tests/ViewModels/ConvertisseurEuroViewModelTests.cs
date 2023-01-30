using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {

        [TestMethod()]
        public void GetDataOnLoadAsyncTest_NotOK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            convertisseurEuro.GetDataOnLoadAsync();

            //Assert
            Assert.IsNull(convertisseurEuro.Devises);
        }

        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);
        }
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            List<Devise> devises = new()
            {
                new(1,"Dollar",1.08),
                new(2,"Franc Suisse",1.07),
                new(3,"Yen",120)
            };

            CollectionAssert.AreEqual(devises, convertisseurEuro.Devises,"Les listes ne sont pas les mêmes");



            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);
        }

        

        [TestMethod()]
        public void ActionSetConversionTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            convertisseurEuro.Montant = 100;
            convertisseurEuro.SelectedDevise = new(2, "Franc Suisse", 1.07);
            convertisseurEuro.ActionSetConversion();
            Assert.AreEqual(107, convertisseurEuro.Resultat);
        }
    }
}