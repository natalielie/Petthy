import React, { Component } from 'react';
import { UseState } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import Axios from 'axios';

import petsTemplate from './PetsTemplate';
import PetApi from '../../services/PetApi';
import ClientApi from '../../services/ClientApi';
import utils from '../../utils';
import DeleteModal from './DeleteModal';

function PetRow(props) {
    const pet = props.pet.pet
    const petLink = `/pets/${pet.petId}`
    //const petOwner = props.client
    //const petOwnerLink = `/clients/${pet.clientId}`

    const getBadge = (status) => {
        return status === 'Active' ? 'success' :
            status === 'Inactive' ? 'secondary' :
                status === 'Pending' ? 'warning' :
                    status === 'Banned' ? 'danger' :
                        'primary'
    }

    return (
        <tr key={pet.petId.toString()}>
            <th scope="row"><Link to={petLink}>{pet.petId}</Link></th>
            <td><Link to={petLink}>{pet.petName}</Link></td>
            <td>{pet.animalKind}</td>
            <td>{pet.petSex}</td>
            <td>{props.pet.owner.firstName}</td>
            <td>{props.pet.owner.lastName}</td>

            <td>
                <Link to={"/pets/edit/" + pet.petId} params={{ pet: pet }}>
                    <Button block color="info" size="sm">Edit</Button>
                </Link>
            </td>
            <td>
                <DeleteModal onDelete={() => props.deletePetHandler(pet.petId)} />
            </td>
        </tr>
    )
}

class Pets extends Component {

    constructor() {
        super();

        this.state = { pets: [] };

        this.petsTemplate = utils.selectTemplateObjectsWithNames(
            petsTemplate, ['petId', 'petName', 'animalKind', 'petSex',
            'clientId', 'clientFirstName', 'clientLastName'])
    }

    componentDidMount() {
        /*var user = JSON.parse(utils.getCookie("user"));
        if (user.role !== 'doctor' && user.role !== 'nurse' && user.role !== 'admin')
            this.props.history.push('/');*/

        document.title = "Pets";
        this.updatePetsHandler();
    }


    updatePetsHandler = () => PetApi.getPetsAndOwners(pets => this.setState({ pets: pets}));

   // updateClientsHandler = () => ClientApi.getClients(clients => this.setState({ clients: clients }));

    addPetHandler = (pet) => PetApi.addPet(pet, this.updatePetsHandler);

    editPetHandler = (pet) => PetApi.editPet(pet, this.updatePetsHandler);

    deletePetHandler = (petId) => PetApi.deletePet(petId, this.updatePetsHandler);


    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> Pets
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">Id</th>
                                            <th scope="col">Pet Name</th>
                                            <th scope="col">Kind</th>
                                            <th scope="col">Gender</th>
                                            <th scope="col">Pet Owner First Name</th>
                                            <th scope="col">Pet Owner Last Name</th>
                                            <th scope="col">Edit</th>
                                            <th scope="col">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.pets.map((pet, index) =>
                                            <PetRow key={index} pet={pet} template={this.petsTemplate} deletePetHandler={this.deletePetHandler} />
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

export default Pets;
