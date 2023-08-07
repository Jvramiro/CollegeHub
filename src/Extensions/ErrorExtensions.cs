using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Security.Cryptography.X509Certificates;

namespace CollegeHub.Extensions {
    public static class ErrorExtensions {

        public static IResult GetErrorMessage(this Exception error) {

            if (error is SQLiteException) {
                return Results.Problem(title: "Database out", statusCode: 500);
            }
            if (error is DbUpdateException) {

                if(error.InnerException.Message.Contains("UNIQUE constraint failed")) {
                    return
                        error.InnerException.Message.Contains("Email") ?
                            Results.Problem(title: "Email is already in use", statusCode: 500) :

                        error.InnerException.Message.Contains("Password") ?
                            Results.Problem(title: "Password is already in use", statusCode: 500) :

                        error.InnerException.Message.Contains("CPF") ?
                            Results.Problem(title: "CPF is already in use", statusCode: 500) :

                            Results.Problem(title: "Any parameter is already in use", statusCode: 500);
                }

            }

            return Results.Problem(title: error.InnerException?.Message, statusCode: 500);
        }

    }
}
