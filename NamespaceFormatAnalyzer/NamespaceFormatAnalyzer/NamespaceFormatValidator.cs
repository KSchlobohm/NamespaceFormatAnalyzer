
namespace NamespaceFormatAnalyzer
{
    public class NamespaceFormatValidator
    {
        public bool IsValid(string namespaceString)
        {
            if (string.IsNullOrWhiteSpace(namespaceString))
            {
                return true;
            }

            if (namespaceString.EndsWith("."))
            {
                // user is not done typing, this is not yet a namespace that can be validated
                return true;
            }

            var namespaceComponents = namespaceString.Split('.');
            for (int i = 1; i < namespaceComponents.Length; i++)
            {
                if (namespaceComponents[i - 1].CompareTo(namespaceComponents[i]) > 0)
                {
                    // the namespace is not sorted alphabetically
                    return false;
                }
            }

            return true;
        }
    }
}