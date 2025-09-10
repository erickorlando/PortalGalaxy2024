export function generateAndDownloadPdf(htmlOrElement, filename) {
    const doc = new jspdf.jsPDF({
        orientation: 'l',
        unit: 'pt',
        format: 'a4'
    });

    return new Promise((resolve, reject) => {
        doc.html(htmlOrElement, {
                callback: doc => {
                    doc.save(filename);
                    resolve();
                }
            });
    });
}