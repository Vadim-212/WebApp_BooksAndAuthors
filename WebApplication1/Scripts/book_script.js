$(document).ready(function()
{
    $.ajax({
        method: `GET`,
        url: `/Author/GetAuthors`,
        success: (data, textStatus, jqXHR) => {
            for (let i = 0; i < data.length; i++) {
                $(`#AuthorId`).append(`<option value="${data[i].Id}">${data[i].FirstName} ${data[i].LastName}</option>`)
            }

        }
    })

    $.ajax({
        method: `GET`,
        url: `/Book/GetGenres`,
        success: (data, textStatus, jqXHR) => {
            for (let i = 0; i < data.length; i++) {
                $(`#GenreId`).append(`<option value="${data[i].Id}">${data[i].Name}</option>`)
            }

        }
    })

    if ($(`#Id`).val() != 0)
    {
        $.ajax({
            method: `GET`,
            url: `/Book/GetBook/${parseInt($(`#Id`).val(), 10)}`,
            success: (data, textStatus, jqXHR) =>
            {
                $(`#AuthorId`).val(data.AuthorId)
                $(`#GenreId`).val(data.GenreId)
                $(`#Title`).val(data.Title)
                $(`#Pages`).val(data.Pages)
                $(`#Price`).val(data.Price)
            }

        })
    }

    $(`#create-edit-book-btn`).on(`click`,(e)=>
    {
        e.preventDefault()
        let obj = {
            Id: $(`#Id`).val(),
            AuthorId: $(`#AuthorId`).val(),
            GenreId: $(`#GenreId`).val(),
            Title: $(`#Title`).val(),
            Pages: $(`#Pages`).val(),
            Price: $(`#Price`).val()
        }
        $.ajax({
            method: `POST`,
            url: `/Book/CreateEditAjax/`,
            data: { obj: JSON.stringify(obj) },
            success: () => {
                location.assign(`/Book/Index`)
            }
        })
    })
})