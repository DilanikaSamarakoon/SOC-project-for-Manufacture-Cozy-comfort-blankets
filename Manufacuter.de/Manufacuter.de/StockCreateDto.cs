// =======================================================================================
// !! FINAL VERSION !!
// This code has been updated to exactly match the method names in your .Designer.cs file.
// Please replace the code in your 'Stock Management.cs' file, then Rebuild the solution.
// This should fix the issue where the buttons and ComboBox do not work.
// =======================================================================================

namespace Manufacuter.de
{
    internal class StockCreateDto
    {
        public int BlanketId { get; set; }
        public int Quantity { get; set; }
        public int ProductionCapacityPerWeek { get; set; }
    }
}