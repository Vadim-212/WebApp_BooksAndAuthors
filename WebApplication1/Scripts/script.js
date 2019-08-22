$(document).ready(function()
{
    $(`#authors-list-search`).on(`input`,function()
    {
        if ($(this).val() == "")
        {
            $(`#authorsList`).children().removeAttr(`selected`)
            $($(`#authorsList`).children()[0]).attr(`selected`, ``)
        }
        for (let i = 0; i < $(`#authorsList`).children().length; i++)
        {
            if ($($(`#authorsList`).children()[i]).text().toLocaleLowerCase().startsWith($(this).val().toLocaleLowerCase()))
            {
                $(`#authorsList`).children().removeAttr(`selected`)
                $($(`#authorsList`).children()[i]).attr(`selected`, ``)
            }
            
        }
    })
})