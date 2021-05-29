// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict"

const TravelDetailsModal = document.querySelector("#travel-details-modal");
let clickedElement;

SetOnClickListener();

function SetOnClickListener() {
    document.addEventListener("click", ManageOnClickEvents);
}

function ManageOnClickEvents(event) {
    clickedElement = event.target;
    
    if (clickedElement.classList.contains("card-title")) {
        ShowTravelDetailsModal();
    } else {
        HideModal();
        RemoveModalText();
    }
}

function ShowTravelDetailsModal(){
    SetTravelModalAttributes();
    GetProductData(GetProductId());
    ShowModal();
}

function GetProductId(){
    return clickedElement.dataset.id;
}

function SetTravelModalAttributes(){
    TravelDetailsModal.setAttribute("style",
        `height: auto; 
        width: 100%; 
        position: fixed; 
        top: 17%; 
        left: 15%;
        padding: 5vh;
        background-color: white; 
        border: black solid 1px;
        border-radius: 10px;`
    );
}

function ShowModal(){
    TravelDetailsModal.classList.remove("hide");
}

function HideModal() {
    TravelDetailsModal.classList.add("hide");
    TravelDetailsModal.removeAttribute("style");
}

function GetProductData(productId){
    const URL = `/Product/GetProductData/${productId}`;
    fetch(URL, {
        method: "GET",
        credentials: "same-origin"
    })
        .then(response => response.json())
        .then(fetchResponse => FillInTravelModal(fetchResponse))
}

function FillInTravelModal(product){
    TravelDetailsModal.insertAdjacentHTML("afterbegin", `
        <h1 id="modal-travel-title">${product.Name}</h1>
        <img src="/img/${product.ImgName}.png" id="modal-travel-img" style="max-height: 350px; width: auto; padding: 3vh" class="img-fluid">
        <p class="card-text"><b>Opis: </b>${product.Description}</p>
        <p class="card-text"><b>Kraj: </b>${product.Country.Name}</p>
        <p class="card-text"><b>Kategoria: </b>${product.ProductCategory.Name}</p>
        <p class="card-text"><b>Biuro podróży: </b>${product.TravelAgency.Name}</p>
        <p class="card-text"><strong>Cena: </strong>${product.DefaultPrice}</p>
        <a href="" type="button" class="btn btn-primary" style="float: bottom">Do Koszyka</a>
    `)
}

function RemoveModalText(){
    let childrenEl = TravelDetailsModal.childNodes;
    while (childrenEl.length > 0){
        childrenEl[0].remove();
    };
}