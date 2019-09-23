$(document).ready(function()
{
    $(`.show-history-link`).on(`click`, function(e)
    {
        e.preventDefault()
        $.ajax(
            {
                method: `GET`,
                url: $(this).attr(`href`),
                success: (data,textStatus,jqXHR) =>
                {
                    console.log(data)
                } 
            }
        )
    })
})