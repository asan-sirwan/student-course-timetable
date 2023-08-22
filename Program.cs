using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using student_course_timetable.Data;
using student_course_timetable.Repositories.AssignRepo;
using student_course_timetable.Repositories.CourseRepo;
using student_course_timetable.Repositories.LecturerRepo;
using student_course_timetable.Repositories.StudentRepo;
using student_course_timetable.Services.AssignService;
using student_course_timetable.Services.CourseService;
using student_course_timetable.Services.LecturerService;
using student_course_timetable.Services.StudentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "AllowReactApp", policy =>
	{
		policy.WithOrigins("http://localhost:5173")
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
	options.AddPolicy(name: "AllowFlutterApp", policy =>
	{
		policy.WithOrigins("http://localhost:56518")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

builder.Services.AddControllers();
builder.Services.AddControllers()
	.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<ILecturerRepository, LecturerRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAssignRepository, AssignRepository>();

builder.Services.AddScoped<ILecturerService, LecturerService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAssignService, AssignService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
	options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
		x => x.UseDateOnlyTimeOnly()
	)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseCors("AllowFlutterApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
