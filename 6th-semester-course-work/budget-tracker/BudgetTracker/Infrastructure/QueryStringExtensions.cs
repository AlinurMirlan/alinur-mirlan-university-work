namespace BudgetTracker.Infrastructure
{
    public static class QueryStringExtensions
    {
        public static RouteValueDictionary GetRoutes(this QueryString queryRoutes)
        {
            var routeDictionary = new RouteValueDictionary();
            if (!queryRoutes.HasValue)
            {
                return routeDictionary;
            }

            var queryStringValues = queryRoutes.Value![1..].Split('&');
            foreach (var queryPair in queryStringValues)
            {
                var queryString = queryPair.Split('=');
                routeDictionary.Add(queryString[0], queryString[1]);
            }

            return routeDictionary;
        }
    }
}
