﻿using project.App.Services;
using project.BL.Facades;
using project.BL.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using project.App.Messages;

namespace project.App.ViewModels.Student;

[QueryProperty(nameof(StudentId), nameof(StudentId))]
public partial class StudentDetailViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>, IRecipient<RefreshManual>
{
    public StudentDetailModel? Student { get; set; } = StudentDetailModel.Empty;
    public Guid StudentId { get; set; } = new();
    public bool StudentView { get; set; } = navigationService.IsStudentLoggedIn;
    public Uri StudentImageUrl { get; set; } = new("https://i.ibb.co/mJrKVdp/student-photo-placeholder.jpg");

    protected override async Task LoadDataAsync()
    {
        Student = await studentFacade.GetAsync(StudentId) ?? Student;
        StudentView = navigationService.IsStudentLoggedIn;

        // Don't display empty URL
        if (Student != null && Student.ImageUrl != null &&
            !Student.ImageUrl.AbsoluteUri.Equals("about:blank", StringComparison.OrdinalIgnoreCase))
            StudentImageUrl = Student.ImageUrl;
    }

    [RelayCommand]
    private async Task EditStudentInfo() => await navigationService.GoToAsync<StudentEditViewModel>(
        new Dictionary<string, object?> { { nameof(StudentId), StudentId } });


    [RelayCommand]
    private async Task DeleteStudent()
    {
        await studentFacade.DeleteAsync(StudentId);
        await navigationService.GoToAsync("//students");
        if(Student != null)
            MessengerService.Send(new StudentEditMessage { StudentId = Student.Id });
    }

    public async void Receive(StudentEditMessage message) => await LoadDataAsync();
    public async void Receive(RefreshManual message) => await LoadDataAsync();

}