import React, { Component } from 'react';
import { UseState } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import Axios from 'axios';
import authService from 'C:/Users/N/FinalPetthy/ClientApp/src/components/api-authorization/AuthorizeService';


import petsTemplate from './PetsTemplate';
import PetApi from '../../services/PetApi';
import ClientApi from '../../services/ClientApi';
import utils from '../../utils';
import DeleteModal from './DeleteModal';
import { useTranslation } from 'react-i18next';
import { withTranslation } from "react-i18next";




async function isAdmin() {
    var user = await authService.getUser();
    const username = user.preferred_username;
    if (username == "admin@gmail.com") {
        return true;
    }
    else {
        return false;

    }
}
function PetRow(props) {

    const pet = props.pet
    const owner = props.pet.owner
    const petLink = `/pets/${pet.pet.petId}`;
    const { t, i18n } = useTranslation();

    if (isAdmin()) {
        return (

            <tr key={pet.pet.petId.toString()}>
                <th scope="row"><Link to={petLink}>{pet.pet.petId}</Link></th>
                <td><Link to={petLink}>{pet.pet.petName}</Link></td>
                <td>{pet.pet.animalKind}</td>
                <td>{pet.pet.petSex}</td>
                <td>{owner.firstName} {owner.lastName}</td>

                <td>
                    <Link to={"/pets/edit/" + pet.pet.petId} params={{ pet: pet }}>
                        <Button block color="info" size="sm">{t("Edit")}</Button>
                    </Link>
                </td>
                <td>
                    <DeleteModal onDelete={() => props.deletePetHandler(pet.pet.petId)} />
                </td>
            </tr>
            )
    }
    if (isAdmin() == false) {
        return (
            <tr key={pet.pet.petId.toString()}>
                <th scope="row"><Link to={petLink}>{pet.pet.petId}</Link></th>
                <td><Link to={petLink}>{pet.pet.petName}</Link></td>
                <td>{pet.pet.animalKind}</td>
                <td>{pet.petSex}</td>
                <td>{owner.firstName} {owner.lastName}</td>
            </tr>
        )
    }
        
}

class Pets extends Component {

    constructor() {
        super();

        this.state = {
            pets: [],
            isAdmin: false };

       // this.petsTemplate = utils.selectTemplateObjectsWithNames(
          //  petsTemplate, ['petId', 'petName', 'animalKind', 'petSex',
           // 'clientId', 'clientFirstName', 'clientLastName'])
    }

    async componentDidMount() {
        var user = await authService.getUser();
        const username = user.preferred_username;
        if (username == "admin@gmail.com") {
            this.state.isAdmin = true;
        }
        else {
            this.state.isAdmin = false;

        }
        document.title = "Pets";
        this.updatePetsHandler();
    }


    updatePetsHandler = () => PetApi.getPetsAndOwners(pets => this.setState({ pets: pets}));

    addPetHandler = (pet) => PetApi.addPet(pet, this.updatePetsHandler);

    editPetHandler = (pet) => PetApi.editPet(pet, this.updatePetsHandler);

    deletePetHandler = (petId) => PetApi.deletePet(petId, this.updatePetsHandler);


    render() {
        const { t } = this.props;
        if (this.state.isAdmin) {
            return (
                <div className="animated fadeIn">
                    <Row>
                        <Col xl={8}>
                            <Card>
                                <CardHeader>
                                    <i className="fa fa-align-justify"></i> {t("Pets")}
                            </CardHeader>
                                <CardBody>
                                    <Table responsive hover>
                                        <thead>
                                            <tr>
                                                <th scope="col">Id</th>
                                                <th scope="col">{t("PetName")}</th>
                                                <th scope="col">{t("Kind")}</th>
                                                <th scope="col">{t("Gender")}</th>
                                                <th scope="col">{t("Pet Owner Name")}</th>
                                                <th scope="col">{t("Edit")}</th>
                                                <th scope="col">{t("Delete")}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {this.state.pets.map((pet, index) =>
                                                <PetRow key={index} pet={pet} deletePetHandler={this.deletePetHandler} />
                                            )}
                                        </tbody>
                                    </Table>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                </div>
            )
        }
        if (this.state.isAdmin == false) {
            return (
                <div className="animated fadeIn">
                    <Row>
                        <Col xl={8}>
                            <Card>
                                <CardHeader>
                                    <i className="fa fa-align-justify"></i> {t("Pets")}
                            </CardHeader>
                                <CardBody>
                                    <Table responsive hover>
                                        <thead>
                                            <tr>
                                                <th scope="col">Id</th>
                                                <th scope="col">{t("PetName")}</th>
                                                <th scope="col">{t("Kind")}</th>
                                                <th scope="col">{t("Gender")}</th>
                                                <th scope="col">{t("Pet Owner Name")}</th>
                                                <th scope="col">{t("Edit")}</th>
                                                <th scope="col">{t("Delete")}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {this.state.pets.map((pet, index) =>
                                                <PetRow key={index} pet={pet} />
                                            )}
                                        </tbody>
                                    </Table>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                </div>
            )
        }
    }
}

export default withTranslation()(Pets);
