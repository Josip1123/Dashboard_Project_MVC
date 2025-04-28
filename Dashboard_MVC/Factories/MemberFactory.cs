using Business.Helpers;
using Dashboard_MVC.Views.Models;
using Data.Entities;

namespace Dashboard_MVC.Factories;

public static class MemberFactory
{
    public static MemberEntity Create(DashboardViewModel vm)
    {
        return new MemberEntity()
        {
            Id = $"M - {IdGenerator.GenerateId(5)}",
            FirstName = vm.CreateMember.FirstName,
            LastName = vm.CreateMember.LastName,
            Email = vm.CreateMember.Email,
            Phone = vm.CreateMember.Phone,
            Title = vm.CreateMember.Title,
        };
    }
}