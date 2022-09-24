using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularDotNet.Models
{
    public class AppUser
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
        
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [StringLength(100)]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Provider { get; set; }

        public DateTime SignUpDate { get; set; }
    }
}