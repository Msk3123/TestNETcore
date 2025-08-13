namespace WebApplication2.Models;

public class CalculatorModel
{
    public int A { get; set; }
    public int B { get; set; }
    public int Result { get; set; }
    public string Operation { get; set; } = "+"; // Default operation
}