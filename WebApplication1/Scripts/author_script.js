$(document).ready(function()
{
    if ($(`#Id`).val() != "0")
    {
        $.ajax({
            method: `GET`,
            url: `/Author/GetAuthor/`,
            data: parseInt($(`#Id`).val(), 10),
            success: (data,textStatus,jqXHR) =>
            {
                $(`#FirstName`).val(data.FirstName)
                $(`#LastName`).val(data.LastName)
            }
            
        })
        
    }
    $(`#create-edit-author-btn`).on(`click`,(e)=>
    {
        e.preventDefault()

        let obj = {
            Id: $(`#Id`).val(),
            FirstName: $(`#FirstName`).val(),
            LastName: $(`#LastName`).val()
        }
        $.ajax({
            method: `POST`,
            url: `/Author/CreateEditAjax/`,
            data: { obj: JSON.stringify(obj) }
        })
    })
})