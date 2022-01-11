namespace AssignmentAPI.Configurations.Policies
{

    public class PolicyEntry
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Allowed { get; set; } = new List<string>();
    }

    public class PoliciesConfig
    {
        public List<PolicyEntry> AllowPolicies { get; set; } = new List<PolicyEntry>();
    }
}
