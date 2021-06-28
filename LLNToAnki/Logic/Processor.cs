using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LLNToAnki.Business.Logic
{
    public class Processor
    {
        private readonly IDataProvider dataProvider;
        private readonly ISnapshotBL snapshotBL;
        private readonly ITargetSequenceBL targetSequenceBL;
        private readonly IAnkiNoteBL ankiNoteBuilder;
        private readonly IConnectNoteBuilder connectNoteBuilder;
        private readonly IConnectNotePoster connectNotePoster;

        public Processor(IDataProvider dataProvider,
            ISnapshotBL snapshotBL,
            ITargetSequenceBL targetSequenceBL,
            IAnkiNoteBL ankiNoteBuilder,
            IConnectNoteBuilder connectNoteBuilder,
            IConnectNotePoster connectNotePoster
            )
        {
            this.dataProvider = dataProvider;
            this.snapshotBL = snapshotBL;
            this.targetSequenceBL = targetSequenceBL;
            this.ankiNoteBuilder = ankiNoteBuilder;
            this.connectNoteBuilder = connectNoteBuilder;
            this.connectNotePoster = connectNotePoster;
        }


        public int PushToAnkiThroughAPI(string filePath, int nbOfItemsToParse)
        {
            var data = dataProvider.GetAllText(filePath);

            var llnItems = snapshotBL.Create(data);

            var totalCount = nbOfItemsToParse != 0 ? nbOfItemsToParse : llnItems.Count;
            var i = 0;

            foreach (var item in llnItems)
            {
                try
                {
                    var wordItem = targetSequenceBL.Build(item);

                    var ankiNote = ankiNoteBuilder.Create(wordItem);

                    var connectNote = connectNoteBuilder.Build(ankiNote);

                    var body = connectNotePoster.Post(connectNote).Result;

                    System.Console.WriteLine($"{++i} / {totalCount}. Word : {wordItem.Sequence}. ({body})");

                    if (i == nbOfItemsToParse) break;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return llnItems.Count;
        }
    }
}