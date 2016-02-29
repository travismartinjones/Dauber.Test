using System.Linq;
using Fixie.Conventions;

namespace Dauber.Test
{
    public static class ClassExpressionExtensions
    {
        public static ClassExpression NameStartsWith(this ClassExpression expression, params string[] prefixes)
        {
            return expression.Where(type => prefixes.Any(prefix => type.Name.StartsWith(prefix)));
        }
    }
}