using EducationApi.Data;
using EducationApi.Data.Repositories;
using EducationApp.Service.Dtos.GroupDtos;
using EducationApp.Core.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using EducationApp.Service.Interfaces;
using EducationApp.Service.Implementations;
using EducationApp.Api.Middlewares;
using EducationApp.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using EducationApp.Service.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(x=>x.Value.Errors.Count()>0)
        .Select(x=>new RestExceptionErrorItem(x.Key, x.Value.Errors.First().ErrorMessage)).ToList();

        return new BadRequestObjectResult(new {message = (string)null, errors = errors});
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EducationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GroupPostDtoValidator>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
