import React, { Component } from 'react';
import { withTranslation } from 'react-i18next';

export class Home extends Component {
  static displayName = Home.name;
    
    render() {
        const { t } = this.props;
    return (
      <div>
            <h1>{t("This is Petthy Health System")}</h1>
            <p>{t("The health system, which provides the necessary and proper care for your pet")}</p>
        
      </div>
    );
  }
}

export default withTranslation()(Home);
