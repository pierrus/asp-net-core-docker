using System;

namespace Todo.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class TodoItem
    {
        public Int32 Id { get; set; }

        public String Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
