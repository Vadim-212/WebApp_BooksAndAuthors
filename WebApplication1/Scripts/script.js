$(document).ready(function()
{
    $(`#authors-list-search`).hide()
    $(`#authors-list-search-btn`).on(`click`,function(e)
    {
        e.preventDefault()
        $(`#authors-list-search`).show().trigger(`focus`)
        $(this).hide()

    })
    $(`#authors-list-search`).on(`input`,function()
    {
        
        for (let i = 0; i < $(`#authorsList`).children().length; i++)
        {
            if ($($(`#authorsList`).children()[i]).text().toLocaleLowerCase().startsWith($(this).val().toLocaleLowerCase()))
            {
                $(`#authorsList`).children().removeAttr(`selected`)
                $($(`#authorsList`).children()[i]).attr(`selected`, ``)
            }
            
        }
        if ($(this).val() == ``)
        {
            $(`#authorsList`).children().removeAttr(`selected`)
            $($(`#authorsList`).children()[0]).attr(`selected`, ``)
        }
    })
})