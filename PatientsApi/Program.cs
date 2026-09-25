var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => "ke onda kompa ñeri");

var patients= new List<Patient>
{
    new Patient { PatientId = 1, FirstName = "Ana", LastName = "Garcia", Gender = "F", BirthDate = "1990-04-12", City = "Hamilton", Height = 165, Weight = 60},
    new Patient { PatientId = 2, FirstName = "Bruno", LastName = "Diaz", Gender = "M", BirthDate = "1985-11-02", City = "London", Height = 178, Weight = 82},
    new Patient { PatientId = 3, FirstName = "Carla", LastName = "Suarez", Gender = "F", BirthDate = "2001-07-30", City = "Hamilton", Height = 158, Weight = 55},
};


app.MapGet("/patients", (string? city) =>
{
    var result= string.IsNullOrEmpty(city)
        ? patients
        : patients.Where(p => p.City == city).ToList();

    return Results.Ok(result);
});


app.MapGet("/patients/{id}", (int id) =>
{

    var patient = patients.FirstOrDefault(p => p.PatientId == id);
    return patient is null ? Results.NotFound() : Results.Ok(patient);
});

app.MapPost("/patients", (Patient newPatient) =>
{

    newPatient.PatientId = patients.Max(p => p.PatientId) + 1;
    patients.Add(newPatient);

    return Results.Created($"/patients/{newPatient.PatientId}", newPatient);


});


app.Run();

public class Patient
{
    
    public int PatientId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Gender { get; set; } = "";
    public string BirthDate { get; set; } = "";
    public string City { get; set; } = "";
    public int Height { get; set; }
    public int Weight { get; set; }
}