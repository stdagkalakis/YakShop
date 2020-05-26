import React from 'react'

import { Divider, Grid, Segment, Icon } from 'semantic-ui-react'


const Landing = ({ history }) => {

    return (
        <div className='Container'>
            <Segment >
                <Grid columns={2} relaxed='very' >
                    <Grid.Column>
                        <div className="button" onClick={() => history.push("/shepherd")}>
                            <Icon size='big' name='user' />
                            <h3>Shepherd</h3>
                        </div>
                    </Grid.Column>
                    <Grid.Column>
                        <div className="button" onClick={() => history.push("/customer")}>
                            <Icon size='big' name='cart' />
                            <h3>Customer</h3>
                        </div>
                    </Grid.Column>
                </Grid>

                <Divider vertical>Enter as...</Divider>
            </Segment>
        </div >
    )
}

export default Landing;