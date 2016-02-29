namespace Dauber.Test.Sample
{
    public class SampleConvention : TestConvention
    {
        protected override string[] Assemblies => new[] { "Dauber.Test", "Dauber.Test.Sample" };
    }
}