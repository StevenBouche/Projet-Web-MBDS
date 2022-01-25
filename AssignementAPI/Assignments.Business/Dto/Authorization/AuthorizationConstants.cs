using Assignments.DAL.Enumerations;

namespace Assignments.Business.Dto.Authorization
{
    public static class AuthorizationConstants
    {
        public const string ALL = "PROFESSOR, STUDENT, ADMIN";
        public const string PROFESSOR = "PROFESSOR";
        public const string STUDENT = "STUDENT";
        public const string ADMIN = "ADMIN";
        public const string PROFESSOR_STUDENT = "PROFESSOR, STUDENT";
        public const string PROFESSOR_ADMIN = "PROFESSOR, ADMIN";
        public const string STUDENT_ADMIN = "STUDENT, ADMIN";

        public const string AuthorizationPolicy_Professor = "PROFESSOR";
        public const string AuthorizationPolicy_Student = "STUDENT";
        public const string AuthorizationPolicy_Admin = "ADMIN";
        public const string AuthorizationPolicy_Professor_Student = "PROFESSOR_STUDENT";
        public const string AuthorizationPolicy_Professor_Admin = "PROFESSOR_ADMIN";
        public const string AuthorizationPolicy_Student_Admin = "STUDENT_ADMIN";
    }
}
