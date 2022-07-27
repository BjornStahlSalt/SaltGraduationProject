import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import InfoIcon from '@mui/icons-material/Info';
import './DescriptionButton.css';

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'rgba(114, 114, 114, 0.8)',
    border: '1px solid #000',
    boxShadow: 24,
    p: 4,
};

export default function DescriptionButton({ level }) {
    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    return (
        <div className='Level__Description'>
            <Button onClick={ handleOpen } startIcon={ <InfoIcon /> }></Button>
            <Modal
                open={ open }
                onClose={ handleClose }
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={ style }>
                    <Typography id="modal-modal-title" variant="h6" color="common.white" component="h2">
                        { level.title }
                    </Typography>
                    <Typography id="modal-modal-description" color="common.white" sx={ { mt: 2 } }>
                        { level.description }
                    </Typography>
                </Box>
            </Modal>
        </div>
    );
}
