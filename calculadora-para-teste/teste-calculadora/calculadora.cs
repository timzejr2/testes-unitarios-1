namespace teste_calculadora
{
    public class Calculadora
    {
        public double Calcular(string operation, double value1, double value2)
        {
            return operation.ToLower() switch
            {
                "soma" => value1 + value2,
                "subtracao" => value1 - value2,
                "multiplicacao" => value1 * value2,
                "divisao" => value2 != 0 ? value1 / value2 : double.NaN,
                _ => double.NaN
            };
        }
    }
}
