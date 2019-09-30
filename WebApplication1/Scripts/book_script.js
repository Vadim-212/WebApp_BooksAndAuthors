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
})