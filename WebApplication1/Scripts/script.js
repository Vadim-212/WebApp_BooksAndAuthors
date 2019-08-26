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


    $(`#users-list-search`).hide()
    $(`#users-list-search-btn`).on(`click`, function (e) {
        e.preventDefault()
        $(`#users-list-search`).show().trigger(`focus`)
        $(this).hide()

    })
    $(`#users-list-search`).on(`input`, function () {

        for (let i = 0; i < $(`#usersList`).children().length; i++) {
            if ($($(`#usersList`).children()[i]).text().toLocaleLowerCase().startsWith($(this).val().toLocaleLowerCase())) {
                $(`#usersList`).children().removeAttr(`selected`)
                $($(`#usersList`).children()[i]).attr(`selected`, ``)
            }

        }
        if ($(this).val() == ``) {
            $(`#usersList`).children().removeAttr(`selected`)
            $($(`#usersList`).children()[0]).attr(`selected`, ``)
        }
    })

    $(`#books-list-search`).hide()
    $(`#books-list-search-btn`).on(`click`, function (e) {
        e.preventDefault()
        $(`#books-list-search`).show().trigger(`focus`)
        $(this).hide()

    })
    $(`#books-list-search`).on(`input`, function () {

        for (let i = 0; i < $(`#booksList`).children().length; i++) {
            if ($($(`#booksList`).children()[i]).text().toLocaleLowerCase().startsWith($(this).val().toLocaleLowerCase())) {
                $(`#booksList`).children().removeAttr(`selected`)
                $($(`#booksList`).children()[i]).attr(`selected`, ``)
            }

        }
        if ($(this).val() == ``) {
            $(`#booksList`).children().removeAttr(`selected`)
            $($(`#booksList`).children()[0]).attr(`selected`, ``)
        }
    })
})