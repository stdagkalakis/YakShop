import React, { Fragment } from 'react'

import { Card, Image, Segment } from 'semantic-ui-react'
import logo from '../../img/YikYak.png';
const Yak = ({ yak }) => {
    const { name, age } = yak;
    const lastShavedAge = yak["age-last-shaved"];

    return (
        <Fragment>
            {age < 10 &&    // Check if age is less than 10.
                (<Card color='orange'>
                    <Card.Content textAlign='left' >
                        <Image
                            floated='right'
                            size='mini'
                            src={logo}
                        />
                        <Card.Header>{name}</Card.Header>
                        <Card.Meta>Female</Card.Meta>
                    </Card.Content>
                    <Card.Description>
                        <Segment.Group >
                            <Segment textAlign='left'>Age: <strong>{age}</strong></Segment>
                            <Segment textAlign='left'>Last shaved: <strong>{lastShavedAge}</strong></Segment>
                        </Segment.Group >

                    </Card.Description>
                </Card >)
            }
        </Fragment>
    )
}
export default Yak;