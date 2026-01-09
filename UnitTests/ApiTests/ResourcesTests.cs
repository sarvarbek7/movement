using System.Globalization;
using System.Runtime.InteropServices;
using Movement.Api.Resources;

namespace Movement.UnitTests.ApiTests;

public class ResourcesTests
{
    // Tests for resource files can be added here in the future.
    [Theory]
    [InlineData("en", "This is test error message.")]
    [InlineData("ru", "Это тестовое сообщение.")]
    [InlineData("uz", "Bu test xatolik xabari.")]
    public void TestErrorMessagesResourceShouldSuccess(string cultureCode, string expectedMessage)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

        var message = ErrorMessages.Test;
        // Add assertions or checks as needed.
        Assert.Equal(expectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is not the expected message.")]
    [InlineData("ru", "Это не ожидаемое сообщение.")]
    [InlineData("uz", "Bu kutilgan xabar emas.")]
    public void TestErrorMessagesResourceShouldFail(string cultureCode, string unexpectedMessage)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

        var message = ErrorMessages.Test;
        // Add assertions or checks as needed.
        Assert.NotEqual(unexpectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is the test message(info).")]
    [InlineData("ru", "Это тестовое сообщение.")]
    [InlineData("uz", "Bu info test xabari.")]
    public void TestInfoMessagesResourceShouldSuccess(string cultureCode, string expectedMessage)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        var message = InfoMessages.Test;
        // Add assertions or checks as needed.
        Assert.Equal(expectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is not the expected info message.")]
    [InlineData("ru", "Это не ожидаемое информационное сообщение.")]
    [InlineData("uz", "Bu kutilgan info xabar emas.")]
    public void TestInfoMessagesResourceShouldFail(string cultureCode, string unexpectedMessage)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        var message = InfoMessages.Test;
        // Add assertions or checks as needed.
        Assert.NotEqual(unexpectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is a test error message with a placeholder: PlaceholderValue.", "PlaceholderValue")]
    [InlineData("ru", "Это тестовое сообщение с заполнителем: PlaceholderValue.", "PlaceholderValue")]
    [InlineData("uz", "Bu to'ldiruvchi bilan test xatolik xabari: PlaceholderValue.", "PlaceholderValue")]
    [InlineData("en", "This is a test error message with a placeholder: .", "")]
    [InlineData("ru", "Это тестовое сообщение с заполнителем: .", "")]
    [InlineData("uz", "Bu to'ldiruvchi bilan test xatolik xabari: .", "")]
    public void TestErrorMessagesResourceWithPlaceholderShouldSuccess(string cultureCode, string expectedMessage, string placeHolder)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

        var message = ErrorMessages.TestWithPlaceholder(placeHolder);
        // Add assertions or checks as needed.
        Assert.Equal(expectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is not the expected message with a placeholder.", "PlaceholderValue")]
    [InlineData("ru", "Это не ожидаемое сообщение с заполнителем.", "PlaceholderValue")]
    [InlineData("uz", "Bu kutilgan to'ldiruvchi bilan xabar emas.", "PlaceholderValue")]
    [InlineData("en", "This is a test error message with a placeholder: PlaceholderValue.", "WrongPlaceholderValue")]
    [InlineData("ru", "Это тестовое сообщение с заполнителем: PlaceholderValue.", "WrongPlaceholderValue")]
    [InlineData("uz", "Bu to'ldiruvchi bilan test xatolik xabari: PlaceholderValue.", "WrongPlaceholderValue")]
    
    public void TestErrorMessagesResourceWithPlaceholderShouldFail(string cultureCode, string unexpectedMessage, string placeHolder)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

        var message = ErrorMessages.TestWithPlaceholder(placeHolder);
        // Add assertions or checks as needed.
        Assert.NotEqual(unexpectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is a test message(info) with a placeholder: PlaceholderValue.", "PlaceholderValue")]
    [InlineData("ru", "Это тестовое сообщение с заполнителем: PlaceholderValue.", "PlaceholderValue")]
    [InlineData("uz", "Bu to'ldiruvchi bilan info test xabari: PlaceholderValue.", "PlaceholderValue")]
    [InlineData("en", "This is a test message(info) with a placeholder: .", "")]
    [InlineData("ru", "Это тестовое сообщение с заполнителем: .", "")]
    [InlineData("uz", "Bu to'ldiruvchi bilan info test xabari: .", "")]
    public void TestInfoMessagesResourceWithPlaceholderShouldSuccess(string cultureCode, string expectedMessage, string placeHolder)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        var message = InfoMessages.TestWithPlaceholder(placeHolder);
        // Add assertions or checks as needed.
        Assert.Equal(expectedMessage, message);
    }

    [Theory]
    [InlineData("en", "This is not the expected message(info) with a placeholder.", "PlaceholderValue")]
    [InlineData("ru", "Это не ожидаемое сообщение(info) с заполнителем.", "PlaceholderValue")]
    [InlineData("uz", "Bu kutilgan to'ldiruvchi bilan info xabar emas.", "PlaceholderValue")]
    [InlineData("en", "This is a test message(info) with a placeholder: PlaceholderValue.", "WrongPlaceholderValue")]
    [InlineData("ru", "Это тестовое сообщение(info) с заполнителем: PlaceholderValue.", "WrongPlaceholderValue")]
    [InlineData("uz", "Bu to'ldiruvchi bilan info test xabari: PlaceholderValue.", "WrongPlaceholderValue")]
    public void TestInfoMessagesResourceWithPlaceholderShouldFail(string cultureCode, string unexpectedMessage, string placeHolder)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        var message = InfoMessages.TestWithPlaceholder(placeHolder);
        // Add assertions or checks as needed.
        Assert.NotEqual(unexpectedMessage, message);
    }
}