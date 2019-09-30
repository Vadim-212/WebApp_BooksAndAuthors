$(document).ready(function()
{
    $.ajax({
        method: `GET`,
        url: `/Book/GetBooks`,
        success: (data, textStatus, jqXHR) => {
            for (let i = 0; i < data.length; i++) {
                $(`#BooksId`).append(`<option value="${data[i].Id}">${data[i].Title}</option>`)
            }

        }
    })
    $.ajax({
        method: `GET`,
        url: `/User/GetUsers`,
        success: (data, textStatus, jqXHR) => {
            for (let i = 0; i < data.length; i++) {
                $(`#UserId`).append(`<option value="${data[i].Id}">${data[i].Name}</option>`)
            }

        }
    })
})