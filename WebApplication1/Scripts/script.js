$(document).ready(function()
{
    $(`#button1`).on(`click`,() =>
    {
        $.ajax(
            {
                method: `GET`,
                url: `/UsersBooks/Index`,
                success: (data,textStatus,jqXHR) =>
                {
                    console.log(data)
                } 
            }
        )
    })
})