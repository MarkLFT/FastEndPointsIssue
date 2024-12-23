namespace WebApplication1;

public static class ApiConstants
{
    public static class Routes
    {
        #region Currencies

        public const string CurrenciesRoute = "/finance/currencies";
        public const string CurrenciesByIdRoute = "/finance/currencies/{currencyId:int}";
        public const string CurrenciesByCodeRoute = "/finance/currencies/{currencyCode}";
        public const string CurrenciesDeleteRoute = "/finance/currencies/{currencyId:int}";
        public const string CurrenciesAddExchangeRateRoute = "/finance/currencies/addrate";

        #endregion Currencies

        #region TaxCodes

        public const string TaxCodesRoute = "/finance/taxcodes";
        public const string TaxCodesByIdRoute = "/finance/taxcodes/{taxcodeId:int}";
        public const string TaxCodesByCodeRoute = "/finance/taxcodes/{taxcodeCode}";
        public const string TaxCodesDeleteRoute = "/finance/taxcodes/{taxcodeId:int}";

        #endregion TaxCodes

        #region TaxSchemes

        public const string TaxSchemesRoute = "/finance/taxschemes";
        public const string TaxSchemesByIdRoute = "/finance/taxschemes/{taxschemeId:int}";
        public const string TaxSchemesByCodeRoute = "/finance/taxschemes/{taxschemeCode}";
        public const string TaxSchemesDeleteRoute = "/finance/taxschemes/{taxschemeId:int}";

        #endregion TaxSchemes

        #region Account Groups

        public const string AccountGroupsRoute = "/finance/accountgroups";
        public const string AccountGroupByIdRoute = "/finance/accountgroups/{accountgroupId:int}";
        public const string AccountGroupDeleteRoute = "/finance/accountgroups/{accountgroupId:int}";

        #endregion Account Groups

    }
}
