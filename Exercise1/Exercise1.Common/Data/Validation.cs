
// Created but not needed
// Keeping it as a POC
namespace Exercise1.Common.Data
{
    public static class Validation
    {
        public static bool isNumeric(string value){
            return int.TryParse(value, out int n);
        }
    }
}