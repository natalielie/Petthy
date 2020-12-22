import React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { withTranslation } from "react-i18next";

class DeleteModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            modal: false
        };

        this.toggle = this.toggle.bind(this);
    }

    toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }

    render() {
        const { t } = this.props;
        return (
            <div>
                <Button color="danger" size="sm" onClick={this.toggle}>{t("Delete")}</Button>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>{t("Confirmation")}</ModalHeader>
                    <ModalBody>
                        {t("Are you sure?")}
                    </ModalBody>
                    <ModalFooter>
                        <Button color="danger" onClick={() => { this.toggle(); this.props.onDelete() }}>{t("Delete")}</Button>{' '}
                        <Button color="secondary" onClick={this.toggle}>{t("Cancel")}</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

export default withTranslation()(DeleteModal);