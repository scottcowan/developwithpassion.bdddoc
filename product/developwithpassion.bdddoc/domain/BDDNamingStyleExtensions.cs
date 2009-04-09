namespace developwithpassion.bdddoc.domain
{
    public static class BDDNamingStyleExtensions
    {
        public static BDDStyleName as_bdd_style_name(this string item)
        {
            return new BDDStyleName(item);
        }
    }
}