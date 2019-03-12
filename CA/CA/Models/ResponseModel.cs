using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CA.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            ValidationResults = new List<ValidationResult>();
            CredentialModel = new CredentialModel{ UserId = "", Expire = "", Username = "" };
        }
        public CredentialModel CredentialModel { get; set; }
        public IList<ValidationResult> ValidationResults { get; set; }
    }

    public class CredentialModel
    {
        public string UserId { get; set; }
        public string Expire { get; set; }
        public string Username { get; set; }
    }
}
