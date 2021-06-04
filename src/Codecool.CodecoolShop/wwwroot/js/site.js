// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict"

const TravelDetailsModal = document.querySelector("#travel-details-modal");
const CartDetails = document.getElementById("modal");
let clickedElement;

SetOnClickListener();

function SetOnClickListener() {
    document.addEventListener("click", ManageOnClickEvents);
}


function ManageOnClickEvents(event) {
    clickedElement = event.target;
    //event.preventDefault();
    
    if (clickedElement.classList.contains("card-title")) {
        ShowTravelDetailsModal();
        
    }
    else if (clickedElement.classList.contains("add-btn"))
    {
        AddToCart();
    }
    else {
        HideModal();
        RemoveModalText();
    }
}

function ShowTravelDetailsModal(){
    SetTravelModalAttributes();
    GetProductData(GetProductId());
    ShowModal();
}

function FillCartModal(title) {
    CartDetails.insertAdjacentHTML("afterbegin", `
        <h2>Dodałeś do koszyka produkt :</h2>
        <br>
        <h3 id="modal-travel-title">${title}</h3>
        <br>
        <h2>W celu finalizacji zakupu przejdż do zakładki "Koszyk"</h2>  
    `)
}

function AddToCart() {
    AddProductToCart(clickedElement.id);
    SetCartModalAttributes();
    const title = clickedElement.parentNode.childNodes.item(1).textContent;
    FillCartModal(title);
    console.log(title);
    CartDetails.classList.remove("hide");
    
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

function SetCartModalAttributes(title){
    CartDetails.setAttribute("style",
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
    CartDetails.classList.add("hide");
    CartDetails.removeAttribute("style");
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

function AddProductToCart(productId){
    const URL = `/Product/AddToCart/${productId}`;
    fetch(URL, {
        method: "POST",
        credentials: "same-origin"
    })
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
    let childrenElCart = CartDetails.childNodes;
    while (childrenElCart.length > 0){
        childrenElCart[0].remove();
    };
}