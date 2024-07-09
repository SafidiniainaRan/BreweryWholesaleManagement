namespace BreweryWholesaleManagement.Helpers
{
    public class Constantes
    {
        public class Pagination
        {
            public const int PageIndex = 0;
            public const int PageSize = 20;
        }
        public class ErrorMessages
        {
            public const string ElementWithIdNodFound = "{0} whith id {1} not found";
            public const string ElementWithIdMustExist = "{0} whith id {1} must exist";
            public const string WholesalerMustExist = "The wholesaler must exist";
            public const string OrderCannotBeEmpty = "The order cannot be empty";
            public const string NoDuplicatesInOrder = "There cannot be duplicates in the order";
            public const string BeerMustBeSoldByWholesaler = "The beer with id {0} must be sold by the wholesaler";
            public const string QuantityExceedsStock = "The quantity of beer ordered cannot exceed the stock of the wholesaler for the beer with id {0}";
        }
    }
}
