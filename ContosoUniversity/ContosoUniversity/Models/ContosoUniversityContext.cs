using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ContosoUniversityContext : DbContext
    {

        public ContosoUniversityContext() : base("ContosoUniversityContext")
        {

            Database.SetInitializer<ContosoUniversityContext>(new ContosoDbInitializer());

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }

}