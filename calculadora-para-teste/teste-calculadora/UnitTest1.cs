namespace teste_calculadora
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("soma", 2, 3, 5)]
        [InlineData("subtracao", 5, 3, 2)]
        [InlineData("multiplicacao", 4, 3, 12)]
        [InlineData("divisao", 10, 2, 5)]
        public void Test_Calculate(string operation, double value1, double value2, double expected)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Calcular(operation, value1, value2);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}